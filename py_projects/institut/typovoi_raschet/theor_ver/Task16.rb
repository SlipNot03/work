require_relative "Function/my_methods"
require_relative "Function/plotter"

# Задание 16. В таблице заданы частоты появлений значений двумерной дискретной случайной величины (X, Y).
# При уровне значимости alpha = 0.05 найти коэффициент корреляции, проверить его значимость,
# найти линейные уравнения регрессии Y на X и X на Y.
# Также нужно построить корреляционное поле и обе прямые регрессии.
# Вариант 1: значения X и Y занесены ниже.

#Я анализирую связь: Пирсон, Спирмен, регрессия, МНК, R2( коэффициент детерминации), ковариация

# X - значения первого признака.
X_TASK16 = [31.11, 33.04, 33.80, 35.29, 32.61, 32.95, 30.83, 31.81, 35.54, 34.57,
            31.96, 36.00, 32.11, 29.98, 31.35, 37.41, 53.28, 41.88, 33.01, 35.45]
# Y - значения второго признака, идут парами с X.
Y_TASK16 = [39.07, 37.48, 35.92, 33.43, 29.61, 38.35, 28.12, 32.61, 33.51, 34.34,
            37.17, 32.20, 38.70, 37.02, 35.16, 38.31, 31.09, 38.09, 39.63, 37.14]

def main(x_values = X_TASK16, y_values = Y_TASK16)
  # correlation_coefficient - коэффициент корреляции Пирсона.
  correlation_coefficient = MyMethods.coeff_correlation(x_values, y_values)
  puts format("Коэффициент корреляции: %.4f", correlation_coefficient)
  # alpha - уровень значимости из условия.
  alpha = 0.05
  # k - степени свободы для проверки корреляции.
  k = x_values.length - 2
  # r - критическое значение корреляции из таблицы.
  r = MyMethods.search_crit_pearson(alpha: alpha, k: k)
  if correlation_coefficient < r
    puts "Коэффициент корреляции значим на уровне α = #{alpha}"
  else
    puts "Коэффициент корреляции не значим на уровне α = #{alpha}"
  end

  # slope_Y_on_X и intercept_Y_on_X - коэффициенты прямой Y от X.
  slope_y_on_x, intercept_y_on_x = MyMethods.regression_equation_yx(x_values, y_values)
  # slope_X_on_Y и intercept_X_on_Y - коэффициенты прямой X от Y.
  slope_x_on_y, intercept_x_on_y = MyMethods.regression_equation_yx(y_values, x_values)

  puts format("Уравнение регрессии Y на X: Y = %.4f  X + %.4f", slope_y_on_x, intercept_y_on_x)
  puts format("Уравнение регрессии X на Y: X = %.4f  Y + %.4f", slope_x_on_y, intercept_x_on_y)

  Plotter.scatter_with_lines(
    x_values,
    y_values,
    [
      {
        "x" => x_values,
        "y" => x_values.map { |x| slope_y_on_x * x + intercept_y_on_x },
        "label" => "Регрессия Y на X",
        "color" => "red"
      },
      {
        "x" => y_values.map { |y| slope_x_on_y * y + intercept_x_on_y },
        "y" => y_values,
        "label" => "Регрессия X на Y",
        "color" => "green"
      }
    ],
    "task16_regression",
    title: "Корреляционное поле и регрессии"
  )
end

main if __FILE__ == $PROGRAM_NAME
