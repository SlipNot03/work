<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Employee;
use App\Http\Resources\EmployeeResource;

class EmployeeController extends Controller
{
    public function getEmployees(){
        $employees = Employee::with('department')->get();
        return EmployeeResource::collection($employees);
    }
    
    public function addEmployee(Request $request){
        $employee = Employee::create($request->all());
        // После создания загружаем отдел для возврата полного объекта
        $employee->load('department');
        return new EmployeeResource($employee);
    }
    
    public function getEmployee($employee_id){
        try {
            $employee = Employee::with('department')->findOrFail($employee_id);
            return new EmployeeResource($employee);
        } catch (\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
            return response()->json(['message' => 'Employee not found by ID'], 404);
        }
    }
    
    public function editEmployee(Request $request){
        try{
            $employee = Employee::findOrFail($request->post('id'));
            $employee->fill($request->all())->save();
            // Загружаем отдел после обновления
            $employee->load('department');
            return new EmployeeResource($employee);

        } catch(\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
            return response()->json(['message' => 'Employee not found by ID'], 404);
        }
    }
    
    public function deleteEmployee($employee_id){
        try{
            $employee = Employee::findOrFail($employee_id);
            $employee->delete();
            return response()->json(['message' => 'Employee deleted successfully']);

        } catch(\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
            return response()->json(['message' => 'Employee not found by ID'], 404);
        }
    }
}