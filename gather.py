#!/usr/bin/env python3

import io
import sys

print('iters', 'pop', 'mut', 'scross', 'cross',
      'algo', 'min_time', 'max_time', 'avg_time', 'min_res', 'max_res', 'avg_res')

cfg = {}
for line in io.TextIOWrapper(sys.stdin.buffer, encoding='utf-16').readlines():
    line = line.strip()
    if line.endswith('using config:'):
        cfg = {}
        continue

    words = line.split()
    if words[1] == '=':
        cfg[words[0]] = words[2]
        continue

    algo = 0 if words[0] == 'Simple' else 1
    min_time, max_time = words[7][:-1].split('-')
    avg_time = words[9][:-3]

    min_res, max_res = words[11].split('-')
    avg_res = words[13][:-1]

    print(cfg['numberOfIterations'],
          cfg['specimenPoolSize'],
          cfg['mutationChance'],
          cfg['singleFeatureCrossoverChance'],
          cfg['crossoverChance'],
          algo, min_time, max_time, avg_time, min_res, max_res, avg_res)
