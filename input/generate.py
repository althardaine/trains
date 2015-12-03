#!/usr/bin/env python
# coding=utf-8

from __future__ import print_function

import random
import sys
import argparse

parser = argparse.ArgumentParser()
parser.add_argument('-s', '--num-stops', type=int, default=20)
parser.add_argument('-t', '--num-trains', type=int, default=20)
parser.add_argument('-c', '--train-capacity', type=int, default=50)
parser.add_argument('-m', '--max-trains-per-line', type=int, default=10)
ARGS = parser.parse_args(sys.argv[1:])

trains = []

for i in range(ARGS.num_trains):
    path_len = random.randint(2, ARGS.num_stops)
    path = random.sample(range(ARGS.num_stops), path_len)
    trains.append(path)

stops = [0] * ARGS.num_stops
solution = []

for train in trains:
    solution.append(random.randint(1, ARGS.max_trains_per_line))
    for stop in train:
        stops[stop] += solution[-1] * ARGS.train_capacity

print(','.join(str(x) for x in stops))
print(ARGS.num_trains)
print('\n'.join('%d:%s' % (i, ','.join(str(x) for x in path)) for i, path in enumerate(trains)))
print('')
print('numberOfBuses=%d' % sum(solution))
print('busCapacity=%d' % ARGS.train_capacity)

print('\n'.join(str(x) for x in solution), file=sys.stderr)
