require_relative "statistics_helper"


# Тут считаются абсолютные частоты: программа проходит по выборке и показывает, сколько раз встретилось каждое значение.


module Distribution
  module_function

  def main(data)
    # число - сколько раз оно встретилось.
    StatisticsHelper.frequencies(data).each do |x, count|
      puts "#{StatisticsHelper.fmt_number(x)} #{count}"
    end
  end
end
