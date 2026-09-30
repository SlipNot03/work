require_relative "Function/statistics_helper"


# Задание 13. По данным двух выборок нормального закона распределения проверить гипотезу
# о равенстве генеральных средних при конкурирующей гипотезе об их неравенстве.
# В ответе привести средние двух выборок, вычисленное значение критерия, табличное значение и вывод.
# Вариант 1: выборки X и Y занесены ниже, alpha = 0.1.

#Я проверяю гипотезы: дисперсии через Фишера, средние через Z/t, доли через критерий для двух вероятностей

# x - первая выборка.
X_TASK13 = [65.0, 67.8, 26.6, 55.2, 60.9, 57.7, 45.7, 59.5,
            106.3, 74.5, 50.7, 25.0, -18.2, 76.8, 64.9]
# y - вторая выборка.
Y_TASK13 = [68.2, 84.5, 60.3, 27.8, 55.2, 74.6, 107.2, 60.1,
            10.5, 109.6, 24.1, -49.7, 12.9, 29.5]

def main(data_x = X_TASK13, data_y = Y_TASK13, alpha = 0.1)
  # X и Y - средние по первой и второй выборке.
  x_mean = StatisticsHelper.mean(data_x)
  y_mean = StatisticsHelper.mean(data_y)
  d_x = StatisticsHelper.variance(data_x, sample: true)
  d_y = StatisticsHelper.variance(data_y, sample: true)
  n_x = data_x.length
  n_y = data_y.length
  # Z - посчитанное значение критерия.
  z = (x_mean - y_mean) / Math.sqrt(d_x / n_x + d_y / n_y)
  puts z
  puts((1 - alpha) / 2.0)
  z_crit = 1.65
  puts z_crit
  puts(z.abs < z_crit ? "– нет оснований отвергнуть гипотезу" : "– гипотезу отвергают.")
end

main if __FILE__ == $PROGRAM_NAME
