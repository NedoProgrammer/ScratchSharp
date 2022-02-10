<h1 align="center">ScratchSharp</h1></center>
<p align="center">A programming language that compiles to scratch.</p>

## ...what?
Yes, you heard it right! This project can convert a simple  "scripting" language to Scratch blocks.

## ..and why?
Fun. :D

## current state
ScratchSharp is in heavy development and currently has a barebones REPL to execute simple, one-line instructions.

## how does it work?
ScratchSharp uses a modified version of the [Minsk](https://github.com/terrajobst/minsk) compiler. Instead of compiling to IL bytecode, it "simply" creates Scratch blocks.

## give me an example!

```js
import 'looks';
void start()
{
    var a = "hello, world!";
    say(a);
}
```

will convert to:

![ScratchSharp Hello World program result](https://i.imgur.com/kNvdXhm.png)

## special thanks

- [Immo Landwerth](https://github.com/terrajobst) for even making this project possible!
- Jetbrains Rider IDE for fixing my stupid mistakes.

