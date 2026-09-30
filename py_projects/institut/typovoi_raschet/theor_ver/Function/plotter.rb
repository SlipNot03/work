require "json"
require "fileutils"
require "tempfile"


# Это обертка для графиков: она сохраняет данные во временный JSON и запускает Python-скрипт, который делает PNG-файл.


module Plotter
  module_function

  ROOT = File.expand_path("..", __dir__)
  PLOTS_DIR = File.join(ROOT, "plots")
  SCRIPT = File.join(ROOT, "plotting", "plotter.py")

  def python_bin
    return ENV["PYTHON"] if ENV["PYTHON"] && !ENV["PYTHON"].empty?

    venv_candidates = [
      File.expand_path("../.venv/bin/python", ROOT),
      File.expand_path("../Theor/.venv/bin/python", ROOT)
    ]
    venv_python = venv_candidates.find { |path| File.executable?(path) }
    return venv_python if venv_python

    "python3"
  end

  def task_name
    location = caller_locations.find { |item| File.basename(item.path).match?(/\ATask\d+\.rb\z/) }
    return "plot" unless location

    File.basename(location.path, ".rb").downcase
  end

  def save(payload, file_name)
    FileUtils.mkdir_p(PLOTS_DIR)
    output = File.join(PLOTS_DIR, "#{file_name}.png")
    Tempfile.create(["plot_data", ".json"]) do |file|
      file.write(JSON.generate(payload.merge("output" => output)))
      file.flush
      system(python_bin, SCRIPT, file.path, exception: true)
    end
    output
  rescue StandardError => e
    warn "Не удалось построить график #{file_name}: #{e.message}"
    nil
  end

  def histogram(data, file_name = "#{task_name}_histogram", bins: 30, title: "полигон частот")
    save(
      {
        "type" => "histogram",
        "data" => data,
        "bins" => bins,
        "title" => title,
        "xlabel" => "Значения",
        "ylabel" => "Количество"
      },
      file_name
    )
  end

  def ecdf(data, file_name = "#{task_name}_ecdf", title: "эмпирическая функция распределения")
    save(
      {
        "type" => "ecdf",
        "data" => data,
        "title" => title,
        "xlabel" => "Значения",
        "ylabel" => "Вероятность"
      },
      file_name
    )
  end

  def scatter_with_lines(x, y, lines, file_name, title:, xlabel: "X", ylabel: "Y")
    save(
      {
        "type" => "scatter_lines",
        "x" => x,
        "y" => y,
        "lines" => lines,
        "title" => title,
        "xlabel" => xlabel,
        "ylabel" => ylabel
      },
      file_name
    )
  end

  def line_series(series, file_name, title:, xlabel: "x", ylabel: "y", scatter: nil)
    save(
      {
        "type" => "line_series",
        "series" => series,
        "scatter" => scatter,
        "title" => title,
        "xlabel" => xlabel,
        "ylabel" => ylabel
      },
      file_name
    )
  end

  def histogram_with_polygon(data, bins, file_name, title:)
    save(
      {
        "type" => "histogram_polygon",
        "data" => data,
        "bins" => bins,
        "title" => title,
        "xlabel" => "Значения",
        "ylabel" => "Плотность вероятности"
      },
      file_name
    )
  end

  def histogram_with_curve(data, bins, x, y, file_name, title:)
    save(
      {
        "type" => "histogram_curve",
        "data" => data,
        "bins" => bins,
        "curve" => { "x" => x, "y" => y, "label" => "Теоретическая плотность вероятности" },
        "title" => title,
        "xlabel" => "Значения",
        "ylabel" => "Плотность вероятности"
      },
      file_name
    )
  end

  def ecdf_with_curve(data, x, y, file_name, title:)
    save(
      {
        "type" => "ecdf_curve",
        "data" => data,
        "curve" => { "x" => x, "y" => y, "label" => "Теоретическая функция распределения" },
        "title" => title,
        "xlabel" => "Значения",
        "ylabel" => "Частота"
      },
      file_name
    )
  end
end
