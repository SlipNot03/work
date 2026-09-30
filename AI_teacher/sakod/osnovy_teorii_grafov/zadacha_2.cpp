#include <iostream>
#include <vector>
#include <queue>
#include <string>

using namespace std;

void bfs(int start, const vector<string>& graph, vector<int>& used) {
    int n = (int)graph.size();
    queue<int> vertices;

    used[start] = 1;
    vertices.push(start);

    while (!vertices.empty()) {
        int current = vertices.front();
        vertices.pop();

        for (int to = 0; to < n; to++) {
            if (graph[current][to] == '1' && used[to] == 0) {
                used[to] = 1;
                vertices.push(to);
            }
        }
    }
}

int main() {
    int n;
    cin >> n;

    vector<string> graph(n);
    for (int i = 0; i < n; i++) {
        cin >> graph[i];
    }

    vector<int> used(n, 0);
    int components = 0;

    for (int i = 0; i < n; i++) {
        if (used[i] == 0) {
            components++;
            bfs(i, graph, used);
        }
    }

    cout << components;

    return 0;
}

/*
Пример входных данных:
5
01010
10100
01001
10001
00110

Пример выходных данных:
1

Сложность:
по времени: O(n^2), потому что для каждой вершины просматривается строка матрицы;
по памяти: O(n^2), для хранения матрицы смежности.
*/
