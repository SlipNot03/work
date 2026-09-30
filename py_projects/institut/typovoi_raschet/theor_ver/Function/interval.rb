require_relative "statistics_helper"


# Тут считаются относительные частоты: частота каждого значения делится на общее количество элементов выборки.


module Interval
  module_function

  def main(data)
    # число - его относительная частота.
    n = data.length.to_f
    StatisticsHelper.frequencies(data).each do |x, count|
      puts "#{StatisticsHelper.fmt_number(x)} #{count / n}"
    end
  end
end
