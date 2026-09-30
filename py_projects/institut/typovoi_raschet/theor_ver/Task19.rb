require_relative "Function/statistics_helper"
require_relative "Function/plotter"


# Задание 19. Используя метод наименьших квадратов, найти параметры линейной,
# квадратичной и показательной зависимостей аппроксимирующей функции.
# Определить, какая функция является лучшим приближением зависимости между x и y.
# Вариант 1: данные x и y занесены ниже.

#Я анализирую связь: Пирсон, Спирмен, регрессия, МНК, R2( коэффициент детерминации), ковариация

# x - значения аргумента.
X_TASK19 = [2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0]
# y - значения функции из таблицы.
Y_TASK19 = [16.9, 19.5, 24.5, 31.0, 35.2, 41.3, 48.2, 57.0, 64.6, 72.3]

def solve_linear_system(matrix, vector)
  a = matrix.map(&:dup)
  b = vector.dup
  n = b.length
  n.times do |i|
    pivot = (i...n).max_by { |row| a[row][i].abs }
    a[i], a[pivot] = a[pivot], a[i]
    b[i], b[pivot] = b[pivot], b[i]
    div = a[i][i]
    (i...n).each { |j| a[i][j] /= div }
    b[i] /= div
    n.times do |row|
      next if row == i
      factor = a[row][i]
      (i...n).each { |j| a[row][j] -= factor * a[i][j] }
      b[row] -= factor * b[i]
    end
  end
  b
end

def main
  x = X_TASK19
  y = Y_TASK19
  # linear_params - параметры прямой.
  a1, a0 = StatisticsHelper.regression_equation(x, y, print_roots: false)
  linear_params = [a0, a1]
  y_linear = x.map { |value| a0 + a1 * value }

  sx = x.sum
  sx2 = x.sum { |v| v**2 }
  sx3 = x.sum { |v| v**3 }
  sx4 = x.sum { |v| v**4 }
  sy = y.sum
  sxy = x.zip(y).sum { |vx, vy| vx * vy }
  sx2y = x.zip(y).sum { |vx, vy| vx**2 * vy }
  # quadratic_params - параметры параболы.
  quadratic_params = solve_linear_system(
    [[x.length.to_f, sx, sx2], [sx, sx2, sx3], [sx2, sx3, sx4]],
    [sy, sxy, sx2y]
  )
  y_quadratic = x.map { |value| quadratic_params[0] + quadratic_params[1] * value + quadratic_params[2] * value**2 }

  log_y = y.map { |value| Math.log(value) }
  exp_b, exp_log_a = StatisticsHelper.regression_equation(x, log_y, print_roots: false)
  exponential_params = [Math.exp(exp_log_a), exp_b]
  y_exponential = x.map { |value| exponential_params[0] * Math.exp(exponential_params[1] * value) }

  r2_linear = StatisticsHelper.r_squared(y, y_linear)
  r2_quadratic = StatisticsHelper.r_squared(y, y_quadratic)
  r2_exponential = StatisticsHelper.r_squared(y, y_exponential)

  puts format("Линейная модель: параметры = [%s], R² = %.4f", linear_params.join(" "), r2_linear)
  puts format("Квадратичная модель: параметры = [%s], R² = %.4f", quadratic_params.join(" "), r2_quadratic)
  puts format("Показательная модель: параметры = [%s], R² = %.4f", exponential_params.join(" "), r2_exponential)

  Plotter.line_series(
    [
      { "x" => x, "y" => y_linear, "label" => "Линейная модель", "color" => "blue" },
      { "x" => x, "y" => y_quadratic, "label" => "Квадратичная модель", "color" => "green" },
      { "x" => x, "y" => y_exponential, "label" => "Показательная модель", "color" => "red" }
    ],
    "task19_models",
    title: "Аппроксимация зависимостей",
    scatter: { "x" => x, "y" => y, "label" => "Данные" }
  )
end

main if __FILE__ == $PROGRAM_NAME
