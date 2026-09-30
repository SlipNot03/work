import pandas as pd
import numpy as np
from kmeans import KMeans
import matplotlib.pyplot as artist


class Main:
    def run(self):
        data = pd.read_csv('data/iris.csv')
        
        X = data[['sepal_length', 'sepal_width', 'petal_length', 'petal_width']].values
        y = data['species'].values
        k = int(input("Введите k: "))
        kmeans = KMeans(k)
        expectations, centres, quality_rezult = kmeans.fit(X)
        print("Центры кластеров:")
        print(centres)
        print("Ожидания:")
        print(expectations)
        print("Качество кластеризации:")
        print(quality_rezult)

        #Для графика используем признаки лепестка
        feature_x = 2
        feature_y = 3

        for species in np.unique(y):
            mask = y == species

            artist.scatter(
                X[mask, feature_x],
                X[mask, feature_y],
                label=species,
                alpha=0.7,
            )

        artist.scatter(
            centres[:, feature_x],
            centres[:, feature_y],
            marker="x",
            color="black",
            s=200,
            linewidths=3,
            label="Центры кластеров",
        )

        artist.title("Кластеризация Iris методом k-means")
        artist.xlabel("Длина лепестка")
        artist.ylabel("Ширина лепестка")
        artist.grid(True)
        artist.legend()
        artist.tight_layout()
        artist.savefig("clusters.png")
        artist.close()

        return expectations, centres, quality_rezult


if __name__ == "__main__":
    Main().run()