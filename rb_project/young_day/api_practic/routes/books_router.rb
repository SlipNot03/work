# routes/books_router.rb
require_relative '../services/books_service'
require_relative '../app' # Подключаем наш базовый класс

class BooksRouter < App
  

  get '/' do
    books = BooksService.all_books
    json books
  end

  post '/create' do
    # Метод json_params доступен, потому что он есть в App
    payload = json_params 
    new_book = BooksService.create_book(payload)
    status 201
    json new_book
  end
  put '/update/:id' do 
    id = params['id'].to_i
    payload = json_params
    # find_book = BookService.find_book(id)
    BooksService.update(id,payload)
    
    
  end
  # ... остальные методы ...
end
