---
title: "윈도우 에서 Jekyll 블로그 로컬 서버 돌리기"
comments: true
categories:
  - 테스트
  - 기록
tags:
  - Blog
  - Jekyll
  - Command
---
윈도우에서 지킬을 돌려보려고 뻘짓을 하다가 결국 해냈다.
커맨드로 조작하는 건 왕초보라 모르는 것이 너무 많았다.
나중에 기억하기 위해 커맨드를 돌리는 순서를 기록해두려 한다. ~~(내가 볼라고)~~

## 1. 인코딩 에러 수정
```
chcp 65001
```

무슨 명령인지는 잘 모르겠지만 이 코드를 치면 인코딩 에러가 사라진다.

## 2. D드라이브 접근
```
D:
```
D드라이브를 주로 쓰고 있어서 D드라이브에 대부분의 폴더를 저장하고 있는데, 다른 드라이브는 cd를 써서 바로 접근이 안되더라.
저렇게 단독으로 쳐줘서 D드라이브에 접근한다.

## 3. 로컬 저장소 접근
```
cd D:\Sanghwa\Documents\sangh518.github.io
```
git과 연동된 지킬 블로그가 있는 로컬 저장소에 접근해준다. 

## 4. 지킬 서버 실행
```
bundle exec jekyll serve
```
다른 사람을은 `jekyll serve`만 쳐도 잘 되던데 나는 저렇게 치니까 에러가 뜨고 `bundle exec`를 같이 치라고 뜨더라.
같이 치니까 잘된다.


-------

## Profit!
```
Active code page: 65001
PS C:\Users\Sanghwa> D:
PS D:\> cd D:\Sanghwa\Documents\sangh518.github.io
PS D:\Sanghwa\Documents\sangh518.github.io>  bundle exec jekyll serve
Configuration file: D:/Sanghwa/Documents/sangh518.github.io/_config.yml
            Source: D:/Sanghwa/Documents/sangh518.github.io
       Destination: D:/Sanghwa/Documents/sangh518.github.io/_site
 Incremental build: disabled. Enable with --incremental
      Generating...
                    done in 11.062 seconds.
  Please add the following to your Gemfile to avoid polling for changes:
    gem 'wdm', '>= 0.1.0' if Gem.win_platform?
 Auto-regeneration: enabled for 'D:/Sanghwa/Documents/sangh518.github.io'
    Server address: http://127.0.0.1:4000
  Server running... press ctrl-c to stop.
```

이제 [http://localhost:4000/](http://localhost:4000/)로 접근하면 끝.

잘돌아 간다. 위~히!

추가적으로 안건데 한 로컬 주소에 접근할 때 `Tab`키를 누르면 자동완성이 된다.
또 원하는 폴더를 `shift + 오른쪽키`하면 바로 커맨드로 접근할 수 있다.

