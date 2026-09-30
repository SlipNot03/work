require_relative "statistics_helper"


# Тут считаются основные числовые характеристики выборки: среднее, дисперсия, стандартное отклонение и коэффициент вариации.


module MainCharacter
  module_function

  def main(data)
    # Основные числовые характеристики выборки.
    freq = StatisticsHelper.frequencies(data)
    n = data.length.to_f
    mean = freq.sum { |x, count| x * count } / n
    puts "xв = #{mean}"

    variance = freq.sum { |x, count| (x - mean) ** 2 * count } / n
    puts "s = #{variance}"
    std = Math.sqrt(variance)
    puts "o = #{std}"
    puts "V = #{std / mean * 100}"
  end
end
