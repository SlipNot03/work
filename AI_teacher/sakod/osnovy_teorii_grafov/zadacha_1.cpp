#include <iostream>
#include <vector>

using namespace std;

void printMatrix(const vector<vector<int> >& graph) {
    int n = (int)graph.size();

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            cout << graph[i][j];
            if (j + 1 < n) {
                cout << ' ';
            }
        }
        cout << '\n';
    }
}

void printAdjacencyList(const vector<vector<int> >& graph) {
    int n = (int)graph.size();

    for (int i = 0; i < n; i++) {
        int count = 0;
        for (int j = 0; j < n; j++) {
            if (graph[i][j] == 1) {
                count++;
            }
        }

        cout << count;
        for (int j = 0; j < n; j++) {
            if (graph[i][j] == 1) {
                cout << ' ' << j + 1;
            }
        }
        cout << '\n';
    }
}

void printEdgeList(const vector<vector<int> >& graph) {
    int n = (int)graph.size();

    for (int i = 0; i < n; i++) {
        for (int j = i + 1; j < n; j++) {
            if (graph[i][j] == 1) {
                cout << i + 1 << ' ' << j + 1 << '\n';
            }
        }
    }
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n, m, form;
    cin >> n >> m >> form;

    vector<vector<int> > graph(n, vector<int>(n, 0));

    if (form == 1) {
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                cin >> graph[i][j];
            }
        }
    } else if (form == 2) {
        for (int i = 0; i < n; i++) {
            int count;
            cin >> count;

            for (int j = 0; j < count; j++) {
                int to;
                cin >> to;
                graph[i][to - 1] = 1;
                graph[to - 1][i] = 1;
            }
        }
    } else {
        for (int i = 0; i < m; i++) {
            int from, to;
            cin >> from >> to;
            graph[from - 1][to - 1] = 1;
            graph[to - 1][from - 1] = 1;
        }
    }

    for (int outputForm = 1; outputForm <= 3; outputForm++) {
        if (outputForm == form) {
            continue;
        }

        if (outputForm == 1) {
            printMatrix(graph);
        } else if (outputForm == 2) {
            printAdjacencyList(graph);
        } else {
            printEdgeList(graph);
        }

        if (outputForm < 3) {
            cout << '\n';
        }
    }

    return 0;
}

/*
Пример входных данных:
5 6 2
2 2 3
3 1 3 4
3 1 2 4
3 2 3 5
1 4

Пример выходных данных:
0 1 1 0 0
1 0 1 1 0
1 1 0 1 0
0 1 1 0 1
0 0 0 1 0

1 2
1 3
2 3
2 4
3 4
4 5

Сложность:
по времени: O(n^2 + m);
по памяти: O(n^2), для матрицы смежности.
*/
