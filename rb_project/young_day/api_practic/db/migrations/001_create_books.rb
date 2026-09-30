Sequel.migration do
  change do
    create_table(:books) do
      primary_key :id
      String :author, null: false 
      String :title, null: false
      String :isbn, null: false, unique: true 
    end
  end
end