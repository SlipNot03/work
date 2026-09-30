require_relative "Function/statistics_helper"
require_relative "Function/plotter"


# Задание 21. Провести статистический анализ двумерных данных.
# Вариант 1: пары значений (X, Y) занесены ниже. Нужно найти средние, дисперсии,
# стандартные отклонения, ковариацию, корреляцию и уравнения регрессии.

#Я анализирую связь: Пирсон, Спирмен, регрессия, МНК, R2( коэффициент детерминации), ковариация

# data - пары значений X и Y из варианта.
DATA_TASK21 = [
  [405, 142], [115, 190], [180, 90], [440, 280], [25, 382],
  [360, 160], [443, 270], [330, 270], [0, 360], [250, 490],
  [70, 395], [90, 440], [105, 50], [225, 65], [238, 273],
  [455, 60], [0, 545], [280, 35], [0, 180], [458, 0],
  [25, 260], [0, 325], [320, 0], [180, 150], [460, 275],
  [30, 450], [475, 440], [293, 450], [200, 475], [499, 160],
  [254, 0], [227, 0], [370, 220], [0, 90], [455, 0]
]

def main(sample = DATA_TASK21)
  # x и y - два столбца из этих пар.
  x = sample.map(&:first)
  y = sample.map(&:last)

  mean_x = StatisticsHelper.mean(x)
  mean_y = StatisticsHelper.mean(y)
  var_x = StatisticsHelper.variance(x, sample: true)
  var_y = StatisticsHelper.variance(y, sample: true)
  std_x = Math.sqrt(var_x)
  std_y = Math.sqrt(var_y)
  covariance = StatisticsHelper.covariance(x, y)
  correlation = StatisticsHelper.pearson_correlation(x, y)
  p_value = 2.0 * (1.0 - StatisticsHelper.normal_cdf(correlation.abs * Math.sqrt(sample.length - 2)))
  slope_yx, intercept_yx = StatisticsHelper.regression_equation(x, y, print_roots: false)
  slope_xy, intercept_xy = StatisticsHelper.regression_equation(y, x, print_roots: false)

  puts "n = #{sample.length}"
  puts format("Среднее X = %.4f", mean_x)
  puts format("Среднее Y = %.4f", mean_y)
  puts format("Дисперсия X = %.4f", var_x)
  puts format("Дисперсия Y = %.4f", var_y)
  puts format("Среднее квадратическое отклонение X = %.4f", std_x)
  puts format("Среднее квадратическое отклонение Y = %.4f", std_y)
  puts format("Ковариация = %.4f", covariance)
  puts format("Коэффициент корреляции Пирсона = %.4f", correlation)
  puts format("p-значение = %.4f", p_value)
  puts format("Уравнение регрессии Y на X: y = %.4fx + %.4f", slope_yx, intercept_yx)
  puts format("Уравнение регрессии X на Y: x = %.4fy + %.4f", slope_xy, intercept_xy)

  x_line = StatisticsHelper.linspace(x.min, x.max, 100)
  Plotter.scatter_with_lines(
    x,
    y,
    [
      {
        "x" => x_line,
        "y" => x_line.map { |value| slope_yx * value + intercept_yx },
        "label" => "Регрессия Y на X",
        "color" => "red"
      }
    ],
    "task21_regression",
    title: "Статистический анализ двумерных данных"
  )
end

main if __FILE__ == $PROGRAM_NAME
