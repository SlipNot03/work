ROOT = File.expand_path(__dir__)
TASKS = (1..21).map { |number| File.join(ROOT, "Task#{number}.rb") }

def main
  TASKS.each_with_index do |task, index|
    puts
    puts "Task#{index + 1} ------------------------------------------------------------------"
    system("ruby", task, exception: true)
  end
end

main if __FILE__ == $PROGRAM_NAME
