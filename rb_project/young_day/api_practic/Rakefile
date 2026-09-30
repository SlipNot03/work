require 'rake'
require 'sequel'
require 'logger'
require_relative 'config/Config'

Sequel.extension :migration
task :connect_db do
  require_relative 'config/database'
  DB.loggers << Logger.new($stdout)
end

namespace :db do

  task :create do
    begin
      db_name = Config::DB_NAME
      user = Config::DB_USERNAME
      password = Config::DB_PASSWORD
      host = Config::DB_HOST
      admin_db = Sequel.connect(adapter: 'postgres', host: host, user: user, password: password, database: 'postgres')
      admin_db.run("CREATE DATABASE \"#{db_name}\"")
      admin_db&.disconnect
    end
  end

  task :drop do
    begin
      db_name = Config::DB_NAME
      user = Config::DB_USERNAME
      password = Config::DB_PASSWORD
      host = Config::DB_HOST
      admin_db = Sequel.connect(adapter: 'postgres', host: host, user: user, password: password, database: 'postgres')
      admin_db["SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = ?", db_name].all
      admin_db.run("DROP DATABASE \"#{db_name}\"")
      admin_db&.disconnect
    end
  end

  task migrate: :connect_db do
    Sequel::Migrator.run(DB, MIGRATIONS_PATH, use_transactions: true)
  end

  task rollback: :connect_db do
    Sequel::Migrator.run(DB, MIGRATIONS_PATH, target: 0, use_transactions: true)
  end

end