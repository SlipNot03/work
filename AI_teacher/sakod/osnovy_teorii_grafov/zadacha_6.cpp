#include <iostream>
#include <vector>
#include <queue>
#include <algorithm>

using namespace std;

int bfs(int start, const vector<vector<int> >& graph,
        vector<int>& distance, vector<int>& parent) {
    int n = (int)graph.size();
    queue<int> vertices;

    distance.assign(n, -1);
    parent.assign(n, -1);

    distance[start] = 0;
    vertices.push(start);

    while (!vertices.empty()) {
        int current = vertices.front();
        vertices.pop();

        for (int i = 0; i < (int)graph[current].size(); i++) {
            int to = graph[current][i];

            if (distance[to] == -1) {
                distance[to] = distance[current] + 1;
                parent[to] = current;
                vertices.push(to);
            }
        }
    }

    int farthest = start;
    for (int i = 0; i < n; i++) {
        if (distance[i] > distance[farthest]) {
            farthest = i;
        }
    }

    return farthest;
}

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

    vector<int> distance;
    vector<int> parent;

    int firstEnd = bfs(0, graph, distance, parent);
    int secondEnd = bfs(firstEnd, graph, distance, parent);

    vector<int> path;
    int current = secondEnd;

    while (current != -1) {
        path.push_back(current);
        if (current == firstEnd) {
            break;
        }
        current = parent[current];
    }

    reverse(path.begin(), path.end());

    cout << distance[secondEnd] << '\n';

    for (int i = 0; i < (int)path.size(); i++) {
        cout << path[i] + 1 << ' ';
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

Пример выходных данных:
2
1 3 4

Сложность:
по времени: O(n + m), выполняются два обхода BFS;
по памяти: O(n + m), для списка смежности, очереди и массивов BFS.
*/
