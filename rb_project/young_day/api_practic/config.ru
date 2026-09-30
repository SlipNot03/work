# config.ru

# 1. Загружаем наш главный класс App (он сам подтянет базу и модели)
require_relative 'app'

# 2. Загружаем роутеры
require_relative 'routes/books_router'
# require_relative 'routes/readers_router'

# 3. Настраиваем маршруты
map '/api/books' do
  run BooksRouter
end

# Пример для будущего
# map '/api/readers' do
#   run ReadersRouter
# end