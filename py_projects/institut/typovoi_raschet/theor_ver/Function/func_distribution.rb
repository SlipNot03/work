require_relative "statistics_helper"


# Тут строится эмпирическая функция распределения в текстовом виде: по шагам накапливаются относительные частоты.


module FuncDistribution
  module_function

  def main(data)
    # Эмпирическая функция распределения через накопленные частоты.
    sorted = data.sort
    n = sorted.length.to_f
    last_positions = {}
    sorted.each_with_index { |x, i| last_positions[x] = (i + 1) / n }
    unique = sorted.uniq
    puts format(" x < %3.2f p = %d", unique.first, 0)
    unique.each do |x|
      puts format(" x > %3.2f p =  %1.2f", x, last_positions[x])
    end
  end
end
