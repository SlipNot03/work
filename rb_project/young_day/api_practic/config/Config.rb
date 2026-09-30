require "dotenv/load"

class Config
  DB_USERNAME = ENV["DB_USERNAME"]  || raise("not found DB_USERNAME in .env")
  DB_PASSWORD = ENV["DB_PASSWORD"] || raise("not found DB_PASSWORD in .env")
  DB_HOST = ENV["DB_HOST"] || raise("not found DB_HOST in .env")
  DB_PORT = ENV["DB_PORT"] || raise("not found DB_PORT in .env")
  DB_NAME = ENV["DB_NAME"] || raise("not found DB_NAME in .env")
  

  def self.db_string
    "postgres://#{DB_USERNAME}:#{DB_PASSWORD}@#{DB_HOST}/#{DB_NAME}" 
  end
end
