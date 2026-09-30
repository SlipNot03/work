<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
//работники
class Employee extends Model
{
    protected $fillable=[
        'id',
        'surname',
        'name',
        'patronymic',
        'department_id',
    ];
}
