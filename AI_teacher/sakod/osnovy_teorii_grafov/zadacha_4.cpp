#include <iostream>
#include <vector>

using namespace std;

bool dfs(int vertex, int parentVertex, const vector<vector<int> >& graph,
         vector<int>& color, vector<int>& parent, int& cycleStart, int& cycleEnd) {
    int n = (int)graph.size();
    color[vertex] = 1;

    for (int to = 0; to < n; to++) {
        if (graph[vertex][to] == 0) {
            continue;
        }

        if (to == parentVertex) {
            continue;
        }

        if (color[to] == 0) {
            parent[to] = vertex;

            if (dfs(to, vertex, graph, color, parent, cycleStart, cycleEnd)) {
                return true;
            }
        } else if (color[to] == 1) {
            cycleStart = to;
            cycleEnd = vertex;
            return true;
        }
    }

    color[vertex] = 2;
    return false;
}

int main() {
    int n;
    cin >> n;

    vector<vector<int> > graph(n, vector<int>(n, 0));

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            char c;
            cin >> c;
            graph[i][j] = c - '0';
        }
    }

    vector<int> color(n, 0);
    vector<int> parent(n, -1);

    int cycleStart = -1;
    int cycleEnd = -1;

    for (int i = 0; i < n && cycleStart == -1; i++) {
        if (color[i] == 0) {
            dfs(i, -1, graph, color, parent, cycleStart, cycleEnd);
        }
    }

    vector<int> cycle;
    cycle.push_back(cycleEnd);

    while (cycle.back() != cycleStart) {
        cycle.push_back(parent[cycle.back()]);
    }

    cout << cycle.size() << '\n';

    for (int i = 0; i < (int)cycle.size(); i++) {
        cout << cycle[i] + 1 << ' ';
    }

    return 0;
}

/*
Пример входных данных:
4
0110
1010
1101
0010

Пример выходных данных:
3
3 2 1

Сложность:
по времени: O(n^2), потому что граф задан матрицей смежности;
по памяти: O(n^2), для хранения матрицы.
*/
