# Dots Dungeon
<img width="362" height="342" alt="DiggyBirdicon" src="https://github.com/user-attachments/assets/bfdb0008-5f3d-47d7-9e55-d8248f51ea5c" />

## 프로젝트 소개
- 간단한 설명 : 간단하게 땅을 파며 음식을 먹고, 아이템을 사며 땅 밑으로 내려가는 캐쥬얼 게임입니다.
- 게임 장르 : 3D 캐쥬얼 게임
- 개발 목적
    * 노동력이 필요한 땅파기류 게임을 간단하게 만드는 것이 목적이며
    * 플레이스토에어서 Admob을 이용하여 광고를 게임에 추가해보는 것
- [시연 영상 보기](https://youtu.be/sq_TLDmVvwY)

## 주요 기능
- 커스텀 맵 에디터 : 커스텀 에디터를 이용하여 보다 더 효율적이게 맵을 만들 수 있게 구현 
- Admob : Admob에서 보상형 광고를 이용하여 광고를 보면 보상을 받을 수 있도록 구현
- 최적화
    - 맵 오브젝트는 static batch로 최적화
    - 생성과 삭제가 많은 오브젝트(땅, 음식, 파티클)은 오브젝트 풀을 이용하여 최적화
      
## 사용 기술
- Unity 2022.3.62f1 (3D)
- C#
- Git, Github
