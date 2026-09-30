# services/books_service.rb

class BooksService
  def self.all_books
    # Получаем данные из БД и превращаем в массив хешей
    Book.all.map(&:values)
  end

  def self.create_book(data)
    # data — это уже чистый хэш { 'title' => '...', 'author' => '...' }
    book = Book.create(
      title: data['title'],
      author: data['author'],
      isbn: data['isbn']
    )
    book.values # Возвращаем созданный объект как хэш
  end

  def self.find_book(id)
    Book[id]&.values # Возвращает хэш или nil, если не найдено
  end

  def self.update(id,payload)
    Book[id].update(payload)
  end

  def self.delete_book(id)
    book = Book[id]
    if book
      book.delete
      true
    else
      false
    end
  end
end