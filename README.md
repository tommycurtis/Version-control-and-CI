# SvgGenerator

This project provides a tiny, deterministic SVG generator used as the base
repository for the course. It models images composed of SVG elements and renders
them to a string output.

This repository is intentionally minimal and will be extended throughout the
semester.

## Build

```bash
dotnet build CSCI106.sln
```

## Test

```bash
dotnet test CSCI106.sln
```

## CI Expectations

All assignments require the GitHub Actions CI checks to be passing. The CI
workflow runs the build and test suite on every push and pull request to main.
