require 'sequel'
require 'pg'
require_relative 'Config'
# Подключение
DB = Sequel.connect(Config.db_string)
Sequel::Model.db = DB # Важно для моделей!
MIGRATIONS_PATH = '/home/klenetev/work/rb_project/young_day/test_api2/db' #абсолютный путь к файлам миграций