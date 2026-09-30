@echo off
chcp 65001 >nul
cd /d "%~dp0"

echo Creating Windows virtual environment...
where py >nul 2>nul
if %errorlevel%==0 (
    py -3 -m venv .venv
) else (
    python -m venv .venv
)

if not exist ".venv\Scripts\python.exe" (
    echo Failed to create .venv. Install Python 3 and enable "Add python.exe to PATH".
    pause
    exit /b 1
)

call ".venv\Scripts\activate.bat"
python -m pip install --upgrade pip
python -m pip install -r requirements.txt

echo.
echo Setup complete. You can now run run_heated.bat or run_unheated.bat.
pause
