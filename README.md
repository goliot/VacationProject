# VacationProject
- 1인 게임 개발 프로젝트
---

### 0804
- 3D 오브젝트 무기들로 썸네일을 만드는 프로그램 구현
   - ItemCapture.cs
- 플레이어 Hp바 임시 구현
---

### 0803
- 적 추적이 정상적이지 않던 문제 수정
- 적의 죽음 애니메이션 및 로직 적용
- 로비 -> 로딩 -> 전투 씬 전환 구현
   - 비동기 씬 로드 방식 적용
- 씬 전환시 어둡게 렌더링되던 버그 수정
   - https://codingmania.tistory.com/202
---

### 0802
- 스폰시 위치 초기화가 정상적으로 이루어지지 않던 버그 수정
   - NavAgent가 활성화된 상태에서 강제로 위치를 이동시키다 보니 생겨난 버그
   - 위치를 이동 시킨 후에 활성화 시키는 것으로 해결함
- 거점 점령 시스템 임시 구현
---

### 0801
- 첫 스테이지의 맵 지형 확정
- 지형이 바뀜에 따른 적 이동 루트 등 움직임 로직 수정
- 플레이어측의 미니언 추가
---

### 0730
- 플레이어 더블점프 구현
- 적의 공격 및 데미지 적용 구현
- 플레이어 피격 구현
- Behaviour Tree를 통한 적 AI 테스트
---

### 0728
- Xml 파싱을 통한 캐릭터들의 스탯 불러오기 틀 구현
---

### 0727
- 플레이어 로직 수정
   - 콤보 연속 공격 구현
- 적 피격, 움직임, 추적 개선
---

### 0726
- 오브젝트 풀링을 통한 적 생성 구현
- 적의 공격, 추적 등 움직임 구현
---

### 0725
- FSM 기반의 캐릭터 상태 전이 로직 구현
- Terrain을 통한 맵 구현 연습
---

### 0724
- 오브젝트 풀링을 위한 풀 매니저 적용
- 사용할 에셋 정리
---

### 0721
- 3인칭 팔로잉 카메라 구현
- 메인 캐릭터 점프, 대쉬 등 추가 애니메이션 구현
---

### 0720
- 메인 캐릭터 이동, 애니메이션 적용
