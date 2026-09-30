<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Department;
use App\Http\Resources\DepartmentResource;

class DepartmentController extends Controller
{
    public function getDepartments(){
        $departments = Department::all();
        return DepartmentResource::collection($departments);
    }
    public function addDepartment(Request $request){
        $department = Department::create($request->all());
    }
    public function getDepartment($department_id){
        try {
            $department = Department::findOrFail($department_id);
            return new DepartmentResource($department);
        } catch (\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
            return response('Department not found by ID', 404);
        }
    }
    public function getDepartmentByName($title)
    {
        $departments = Department::where('name', $title)->get();
        
        if ($departments->isEmpty()) {
            return response('No departments found with this title', 404);
        }
        
        return DepartmentResource::collection($departments);
    }
    public function editDepartment(Request $request){
        try{
            $department = Department::findOrFail($request->post('id'));
            $department->fill($request->all())->save();
            return new DepartmentResource($department);

        } catch(\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
            return response('Department not found by ID', 404);
        }
    }
    public function deleteDepartment($department_id){
        try{
            $department = Department::findOrFail($department_id);
            $employees = $department -> employees;
            if (count($employees) == 0)
                $department -> delete();
            else
                return response('Department connected', 404);

        } catch(\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
            return response('Department not found by ID', 404);
        }
    }
}