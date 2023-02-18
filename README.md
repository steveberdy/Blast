# Blast
Weird encoders that I've created.

The output for these encodings are indeterministic, while the decoding output is deterministic. There are many encoded outputs for one input, but always the same decoded output for inputs.

I originally created this in Java while I was sitting bored at school, but I completly rewrote it in C#. Feel free to contribute.

## Building

Run `dotnet build`. This creates a folder named `src/nupkg` with the Nuget package.

Then, run `dotnet tool update -g blast --add-source src/nupkg` from the root of the project. This will install/update the tool as a global tool.

You can now run `blast` in your terminal. Run `blast -h` for help.

## Examples

### Compass Deep Encoder (cdeep)

file.txt:

```
ur mom goes to college
```

Input:

`blast encode -f file.txt -t cdeep`

Output:

file.blst:

```
82kl71jl4il54il60kl52kl5kl32jl61kl70l76kl3jl78jl62il5kl48l58jl47jl47jl74l31kl68
```

### Insert Encoder (insert)

file.txt:

```
ur mom goes to college
```

Input:

`blast encode -f file.txt -t cdeep`

Output:

file.blst:

```
ur instantiate atomizing mom speed ethylate wee goes dispiritedly ballast modestly fuzzy to funny chivied college prosy agist obviated chicane uncovers stigmatized tattles
```
