<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\EmployeeController;
use App\Http\Controllers\DepartmentController;
/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});
Route::get("employees/all",[EmployeeController::class,'getEmployees']);
Route::get("departments/all", [DepartmentController::class, 'getDepartments']);
Route::get("department/{id}", [DepartmentController::class, 'getDepartment']);
Route::get("employee/{id}", [EmployeeController::class, 'getEmployee']);
Route::get("department/search/{name}", [DepartmentController::class, 'getDepartmentByName']);

Route::group(['middleware' => 'auth:sanctum'], function(){
    Route::put("department/edit", [DepartmentController::class, 'editDepartment']);
    Route::put("employee/edit", [EmployeeController::class, 'editEmployee']);
    Route::put("department/add", [DepartmentController::class, 'addDepartment']);
    Route::put("employee/add", [EmployeeController::class, 'addEmployee']);
    Route::put("department/delete/{id}", [DepartmentController::class, 'deleteDepartment']);
    Route::put("employee/delete/{id}", [EmployeeController::class, 'deleteEmployee']);
});
