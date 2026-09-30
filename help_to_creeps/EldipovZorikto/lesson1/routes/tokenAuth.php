<?php
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Validation\ValidationException;
use Illuminate\Http\JsonResponse;

Route::post('/sanctum/token', function (Request $request) {
    // Валидация входных данных (вернет 422 если поля пустые)
    $request->validate([
        'email' => 'required|email',
        'password' => 'required',
        'device_name' => 'required',
    ]);

    $user = User::where('email', $request->email)->first();

    // Проверка учетных данных
    if (! $user || ! Hash::check($request->password, $user->password)) {
        // Вместо ValidationException возвращаем кастомный ответ с 401
        return response()->json([
            'message' => 'Неверный email или пароль',
            'errors' => [
                'email' => ['Предоставленные учетные данные неверны.']
            ]
        ], 401);
    }

    return response()->json([
        'token' => $user->createToken($request->device_name)->plainTextToken,
        'token_type' => 'Bearer'
    ]);
});

Route::get('/sanctum/check', function (Request $request) {
    if (auth('sanctum')->check()) {
        return response()->json([
            'message' => 'Valid token',
            'user' => auth('sanctum')->user()->only('id', 'name', 'email')
        ]);
    } else {
        return response()->json([
            'message' => 'Invalid or missing token'
        ], 401);
    }
});