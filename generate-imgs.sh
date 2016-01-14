#!/bin/sh

for ALGO in 0 1; do
    awk "{ if (\$2 == \"100\" && \$3 == \"0.01\" && \$5 == \"0.01\" && \$6 == \"$ALGO\") { print }}" results.txt > iterations_algo_${ALGO}.txt
done

for ALGO in 0 1; do
    awk "{ if (\$1 == \"100\" && \$3 == \"0.01\" && \$5 == \"0.01\" && \$6 == \"$ALGO\") { print }}" results.txt > population_algo_${ALGO}.txt
done

for ALGO in 0 1; do
    awk "{ if (\$1 == \"100\" && \$2 == \"100\" && \$5 == \"0.01\" && \$6 == \"$ALGO\") { print }}" results.txt > mutation_algo_${ALGO}.txt
done

for ALGO in 0 1; do
    awk "{ if (\$1 == \"100\" && \$2 == \"100\" && \$3 == \"0.01\" && \$6 == \"$ALGO\") { print }}" results.txt > crossover_algo_${ALGO}.txt
done

gnuplot plot.gnuplot
