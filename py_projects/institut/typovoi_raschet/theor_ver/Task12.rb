require_relative "Function/statistics_helper"
require_relative "Function/fisher"


# Задание 12. По двум выборкам нормальных законов распределения проверить гипотезу
# о равенстве дисперсий при конкурирующей гипотезе об их неравенстве.
# Нужно определить дисперсии двух выборок, вычисленное и теоретическое значение критерия,
# а также сделать вывод о принятии или непринятии гипотезы.
# Вариант 1: выборки X и Y занесены ниже, уровень значимости alpha = 0.1.

#Я проверяю гипотезы: дисперсии через Фишера, средние через Z/t, доли через критерий для двух вероятностей

# alpha - уровень значимости из условия.
ALPHA_TASK12 = 0.1
# x - первая выборка.
X_TASK12 = [61.4, 45.6, 46.4, 47.8, 49.2, 57.6, 38.3, 41.9, 55.7, 61.8]
# y - вторая выборка.
Y_TASK12 = [43.5, 61.6, 56.0, 52.0, 30.2, 58.1, 2.0, 47.8, 51.5]

def main(data_x = X_TASK12, data_y = Y_TASK12, alpha = ALPHA_TASK12)
  # data_x и data_y - две выборки, которые сравниваю.
  puts "#{data_x.length} #{data_y.length}"
  # D_x и D_y - дисперсии первой и второй выборок.
  d_x = StatisticsHelper.variance(data_x, sample: true)
  d_y = StatisticsHelper.variance(data_y, sample: true)
  puts d_x
  puts d_y
  # Тут делаю дисперсии исправленными как в Python-коде.
  d_x *= data_x.length.to_f / (data_x.length - 1)
  d_y *= data_y.length.to_f / (data_y.length - 1)
  # F - критерий Фишера: большая дисперсия делится на меньшую.
  f_value = d_x > d_y ? d_x / d_y : d_y / d_x
  f_crit = Fisher.get_crit(alpha / 2.0, data_x.length, data_y.length)
  puts f_value
  puts f_crit
  puts(f_value < f_crit ? "– нет оснований отвергнуть гипотезу" : "– гипотезу отвергают.")
end

main if __FILE__ == $PROGRAM_NAME
