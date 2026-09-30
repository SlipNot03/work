<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use App\Models\Department;
use App\Models\Employee;
use App\Models\User;
use Illuminate\Support\Facades\DB;
class DatabaseSeeder extends Seeder
{
    /**
     * Заполните базу данных приложения.
     *
     * @return void
     */
    public function run()
    {
        DB::table('users')->truncate();
        $users = [
            [
                'name' => 'Иван Петров',
                'email' => 'petrov@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Мария Сидорова',
                'email' => 'sidorova@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Алексей Козлов',
                'email' => 'kozlov@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Ольга Федорова',
                'email' => 'fedorova@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Дмитрий Никитин',
                'email' => 'nikitin@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Елена Васильева',
                'email' => 'vasileva@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Сергей Смирнов',
                'email' => 'smirnov@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Анна Ковалева',
                'email' => 'kovaleva@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Андрей Морозов',
                'email' => 'morozov@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ],
            [
                'name' => 'Наталья Орлова',
                'email' => 'orlova@example.com',
                'password' => 'password123',
                'status' => 'Пользователь',
            ]
        ];

        foreach ($users as $userData) {
            User::create([
                'name' => $userData['name'],
                'email' => $userData['email'],
                'password' => bcrypt($userData['password']),
                'status' => $userData['status'],
            ]);
        }
         $departments = [
            ['name' => 'общие'],
            ['name' => 'Научно-методический отдел'],
            ['name' => 'Отдел комплектования и научной обработки фондов'],
            ['name' => 'Информационно-библиографический отдел'],
            ['name' => 'Отдел публикационной статистики'],
            ['name' => 'сектор научной обработки фондов'],
            ['name' => 'сектор книгообеспеченности'],
            ['name' => 'сектор регистрации читателей'],
            ['name' => 'сектор информационно-библиографической и наукометрической работы ']
        ];
        foreach ($departments as $department) {
            Department::create($department);
        }
        DB::table('employees')->truncate();
        $employees = [
            [
                'id' => 1,
                'surname' => 'Петров',
                'name' => 'Иван',
                'patronymic' => 'Сергеевич',
                'department_id' => 2
            ],
            [
                'id' => 2,
                'surname' => 'Сидорова',
                'name' => 'Мария',
                'patronymic' => 'Ивановна',
                'department_id' => 2
            ],
            [
                'id' => 3,
                'surname' => 'Козлов',
                'name' => 'Алексей',
                'patronymic' => 'Дмитриевич',
                'department_id' => 2
            ],
            [
                'id' => 4,
                'surname' => 'Федорова',
                'name' => 'Ольга',
                'patronymic' => 'Петровна',
                'department_id' => 2
            ],
            [
                'id' => 5,
                'surname' => 'Никитин',
                'name' => 'Дмитрий',
                'patronymic' => 'Андреевич',
                'department_id' => 2
            ],
            [
                'id' => 6,
                'surname' => 'Васильева',
                'name' => 'Елена',
                'patronymic' => 'Викторовна',
                'department_id' => 8
            ],
            [
                'id' => 7,
                'surname' => 'Смирнов',
                'name' => 'Сергей',
                'patronymic' => 'Олегович',
                'department_id' => 8
            ],
            [
                'id' => 8,
                'surname' => 'Ковалева',
                'name' => 'Анна',
                'patronymic' => 'Сергеевна',
                'department_id' => 8
            ],
            [
                'id' => 9,
                'surname' => 'Морозов',
                'name' => 'Андрей',
                'patronymic' => 'Игоревич',
                'department_id' => 8
            ],
            [
                'id' => 10,
                'surname' => 'Орлова',
                'name' => 'Наталья',
                'patronymic' => 'Владимировна',
                'department_id' => 8
            ],
        ];

        foreach ($employees as $employee) {
            Employee::create($employee);
        }
        
    }

}
