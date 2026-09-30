#include <iostream>
#include <vector>
#include <queue>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n, m;
    cin >> n >> m;

    vector<vector<int> > graph(n);

    for (int i = 0; i < m; i++) {
        int from, to;
        cin >> from >> to;

        graph[from - 1].push_back(to - 1);
        graph[to - 1].push_back(from - 1);
    }

    int start;
    cin >> start;
    start--;

    vector<int> distance(n, -1);
    queue<int> vertices;

    distance[start] = 0;
    vertices.push(start);

    while (!vertices.empty()) {
        int current = vertices.front();
        vertices.pop();

        for (int i = 0; i < (int)graph[current].size(); i++) {
            int to = graph[current][i];

            if (distance[to] == -1) {
                distance[to] = distance[current] + 1;
                vertices.push(to);
            }
        }
    }

    for (int i = 0; i < n; i++) {
        cout << distance[i] << ' ';
    }

    return 0;
}

/*
Пример входных данных:
4 4
1 2
1 3
2 3
3 4
2

Пример выходных данных:
1 0 1 2

Сложность:
по времени: O(n + m), потому что BFS просматривает вершины и ребра;
по памяти: O(n + m), для списка смежности, очереди и массива расстояний.
*/
