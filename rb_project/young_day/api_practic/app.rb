# app.rb
require 'sinatra/base'
require 'sinatra/json'
require 'json'

# 1. Подключаем базу данных и модели (чтобы они были доступны во всем приложении)
require_relative 'config/database'
require_relative 'models/class_book'
# require_relative 'models/class_reader'

class App < Sinatra::Base
  # --- Общие настройки для всех роутеров ---
  
  configure do
    # Включаем логирование запросов в консоль
    enable :logging
    # Не показывать HTML-страницы ошибок, мы же делаем JSON API
    set :show_exceptions, false 
  end

  # --- Общие Хелперы (доступны во всех роутерах) ---
  helpers do
    # Тот самый хелпер для чтения JSON, который мы обсуждали
    def json_params
      JSON.parse(request.body.read)
    rescue
      halt 400, { error: "Invalid JSON format" }.to_json
    end
  end

  
  # Если страница/маршрут не найдены (404)
  not_found do
    content_type :json
    { error: "Endpoint not found" }.to_json
  end

  # Если произошла внутренняя ошибка сервера (500)
  error do
    content_type :json
    { error: "Internal Server Error" }.to_json
  end
end