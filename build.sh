#!/bin/bash

echo "Building and exporting project..."

# Extract version from project.godot
VERSION=$(grep 'config/version=' project.godot | sed 's/config\/version="\(.*\)"/\1/')

echo ==== dist/algonquin-$VERSION.dmg ====
/Applications/Godot_mono.app/Contents/MacOS/Godot --headless --export-debug "macOS" dist/algonquin-$VERSION.app
zip -r "dist/algonquin-$VERSION.zip" "dist/algonquin-$VERSION.app"

echo ==== dist/algonquin-server-$VERSION.dmg ====
/Applications/Godot_mono.app/Contents/MacOS/Godot --headless --export-debug "macOS-server" dist/algonquin-server-$VERSION.app
t-debug "macOS-server" dist/algonquin-server-$VERSION.app
zip -r "dist/algonquin-server-$VERSION.zip" "dist/algonquin-server-$VERSION.app"