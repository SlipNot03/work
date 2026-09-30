<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateEmployeesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('employees', function (Blueprint $table) {
            $table->id();
            $table->timestamps();
            $table->string("surname",255);
            $table->string("name",255);
            $table->string("patronymic",255)->nullable();
            $table->foreignId("department_id")->constrained('departments');
        });
        DB::statement("
            ALTER TABLE employees 
            ADD CONSTRAINT employees_surname_check 
            CHECK (
                surname REGEXP '^[А-Яа-яёЁ\\\\s\\\\-]+$'
                AND surname NOT REGEXP '^--+$'
            )
        ");

        DB::statement("
            ALTER TABLE employees 
            ADD CONSTRAINT employees_name_check 
            CHECK (
                name REGEXP '^[А-Яа-яёЁ\\\\s\\\\-]+$'
                AND name NOT REGEXP '^--+$'
            )
        ");

        DB::statement("
            ALTER TABLE employees 
            ADD CONSTRAINT employees_patronymic_check 
            CHECK (
                patronymic IS NULL OR (
                    patronymic REGEXP '^[А-Яа-яёЁ\\\\s\\\\-]+$'
                AND patronymic NOT REGEXP '^--+$'
                )
            )
        ");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('employees');
    }
}