from sklearn.datasets import load_iris

from loo import LeaveOneOutValidator
import matplotlib.pyplot as artist

class Main:
    def run(self):
        iris = load_iris()
        X = iris.data
        y = iris.target
        
        LOO = LeaveOneOutValidator()
        best_k,errors_by_k = LOO.find_best_k(X, y)
        graph_x = list(errors_by_k.keys())
        graph_y = list(errors_by_k.values())
        errors = errors_by_k[best_k]

        print(f"Оптимальное k: {best_k}")
        print(f"Ошибок LOO: {errors}")
        print(f"Accuracy: {(len(y) - errors) / len(y)}")
        artist.plot(graph_x, graph_y,label="ошибки LOO")
        artist.title("LOO результат")
        artist.xlabel("k")
        artist.ylabel("errors")
        artist.scatter(best_k, errors, color="green",label="лучшее k")
        artist.annotate(
            f"k={best_k}, LOO={errors}",
            (best_k, errors),
            xytext=(10, 25),
            textcoords="offset points",
        )
        artist.grid(True)
        artist.legend()
        artist.savefig("loo_show.png")
        return best_k,errors,(len(y) - errors) / len(y),errors_by_k

if __name__ == "__main__":
    Main().run()