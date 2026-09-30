param(
  [Parameter(Mandatory = $true)]
  [string]$Expression
)

$tabs = Invoke-RestMethod "http://127.0.0.1:9222/json/list"
$tab = $tabs | Where-Object { $_.url -like "*tilda*" } | Select-Object -First 1
if (-not $tab) {
  $tab = $tabs | Select-Object -First 1
}
if (-not $tab) {
  throw "No DevTools tab found"
}

$ws = [System.Net.WebSockets.ClientWebSocket]::new()
$uri = [Uri]$tab.webSocketDebuggerUrl
$ws.ConnectAsync($uri, [Threading.CancellationToken]::None).GetAwaiter().GetResult()

$script:nextId = 1

function Receive-Message {
  $buffer = New-Object byte[] 1048576
  $segment = [ArraySegment[byte]]::new($buffer)
  $builder = [System.Text.StringBuilder]::new()
  do {
    $result = $ws.ReceiveAsync($segment, [Threading.CancellationToken]::None).GetAwaiter().GetResult()
    if ($result.Count -gt 0) {
      [void]$builder.Append([System.Text.Encoding]::UTF8.GetString($buffer, 0, $result.Count))
    }
  } while (-not $result.EndOfMessage)
  return $builder.ToString()
}

function Send-CDP {
  param(
    [string]$Method,
    [hashtable]$Params = @{}
  )

  $id = $script:nextId
  $script:nextId += 1
  $payload = @{
    id = $id
    method = $Method
    params = $Params
  } | ConvertTo-Json -Depth 50 -Compress
  $bytes = [System.Text.Encoding]::UTF8.GetBytes($payload)
  $segment = [ArraySegment[byte]]::new($bytes)
  $ws.SendAsync($segment, [System.Net.WebSockets.WebSocketMessageType]::Text, $true, [Threading.CancellationToken]::None).GetAwaiter().GetResult()

  while ($true) {
    $message = Receive-Message | ConvertFrom-Json
    if ($message.id -eq $id) {
      return $message
    }
  }
}

Send-CDP "Runtime.enable" | Out-Null
Send-CDP "Page.enable" | Out-Null
$response = Send-CDP "Runtime.evaluate" @{
  expression = $Expression
  awaitPromise = $true
  returnByValue = $true
}

$ws.CloseAsync([System.Net.WebSockets.WebSocketCloseStatus]::NormalClosure, "done", [Threading.CancellationToken]::None).GetAwaiter().GetResult()

if ($response.error) {
  $response.error | ConvertTo-Json -Depth 20
  exit 1
}

$result = $response.result.result
if ($result.value -ne $null) {
  $result.value | ConvertTo-Json -Depth 50
} elseif ($result.description) {
  $result.description
} else {
  $response | ConvertTo-Json -Depth 50
}
