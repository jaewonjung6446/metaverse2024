# metaverse2024
메타버스 개발자 경진대회 리포지토리

PPT
[ppt.pptx](https://github.com/user-attachments/files/18394559/ppt.pptx)

보고서
[2024년 메타버스 개발자 경진대회 개발결과보고서_AI THEN TTS(정재원).docx](https://github.com/user-attachments/files/18394567/2024._AI.THEN.TTS.docx)

# 도란도란

‘도란도란’은 AI와 TTS를 활용하여 새로운 대화 경험을 제공하는 메타버스 플랫폼입니다. 사용자는 가상의 섬 ‘도란도’에서 자신의 속마음을 털어놓고 다양한 감정을 표현하며, 개인 맞춤형 다이어리 기능을 통해 자기 성찰과 성장을 도모할 수 있습니다.

---

## 프로젝트 개요

- **프로젝트명**: 도란도란
- **팀명**: AI THEN TTS
- **프로젝트 시연 동영상**: [여기](https://youtu.be/Wyi0KtXQfa8)
- **개발 부문**: 성인부 일반 자유과제
- **개발 목적**: 현대인의 고립감과 외로움을 해소하고 심리적 안정감을 제공

---

## 팀 구성 및 역할

| 이름     | 역할                     | 담당 업무                                       |
|----------|--------------------------|------------------------------------------------|
| 정재원   | 팀장 / 개발 총괄         | AI 인격체 구현, 인격 학습, API 연동            |
| 김대영   | 클라이언트 개발          | 클라이언트 개발                                |
| 이영교   | 클라이언트 개발          | 클라이언트 개발 및 API 연동                    |
| 이지환   | 기획 / 프로젝트 총괄     | BM 설계, 서비스 QA, 사운드 디자인              |
| 주선영   | 디자인                   | 로고 및 UI 디자인, C4D 기반 3D 모델링 (맵, 캐릭터 디자인) |

---

## 개발 환경 및 기술 스택

- **개발 환경**
  - Unity 2023.2.20f1 (C#)
  - Maxon Cinema 4D 26.107
  - Blender 4.0
  - LMMS 1.2.2

- **기술 스택**
  - **Frontend**: Unity (C#)
  - **Backend**: Custom API Integration
  - **AI**: GPT-4 turbo 기반 대화 AI, TTS 연동
  - **3D 디자인**: Maxon Cinema 4D, Blender
  - **사운드**: LMMS

---

## 시스템 구성 및 아키텍처

*(시스템 구성 다이어그램 추가 필요)*

---

## 주요 기능

### 1. 대화 기능
- 사용자와 가상 캐릭터 간의 자연스러운 상호작용을 가능하게 합니다.
- OpenAI GPT-4 turbo 기반으로 사용자 음성을 인식하여 캐릭터의 인격에 맞는 음성 반응을 제공합니다.
- 사용자는 다양한 주제로 NPC와 대화하며 몰입감을 높일 수 있습니다.

### 2. 감정 표현 기능
- 사용자와 NPC 간의 감정 표현을 가능하게 하여 더 깊은 공감을 유도합니다.
- 기쁨, 슬픔, 분노 등의 감정을 몸짓으로 표현할 수 있으며, 이를 통해 보다 풍부한 감정적 경험을 제공합니다.

### 3. 다이어리 기능
- 사용자의 대화와 활동을 요약하여 자동으로 다이어리를 생성합니다.
- 다이어리 형식의 로그 북을 제공하여 사용자가 자신의 활동과 생각을 되돌아볼 수 있습니다.
- 자동 생성된 다이어리는 사용자의 자기 성찰을 촉진하며, 중요한 순간을 기록하는 데 도움을 줍니다.

---

## 기대 효과 및 활용 분야

### 1. 외로움 해소
- 언제든지 대화할 수 있는 AI NPC를 통해 외로움을 느끼는 현대인들에게 친구 같은 존재를 제공합니다.
- AI와의 대화를 통해 대화에 대한 두려움을 줄이고, 실제 사람과의 대화에서도 공감 능력을 향상시킬 수 있습니다.

### 2. 피로 해소
- 타인의 시선을 신경 쓰지 않고 AI NPC와 솔직한 대화를 나누며 감정을 정리할 수 있습니다.
- 가상 섬의 자연 환경(바다, 산 등)을 통해 심리적 안정감을 제공하여 스트레스를 완화합니다.

### 3. 자기 성찰 촉진
- 자동으로 생성되는 다이어리를 통해 사용자는 자신의 행동과 감정을 되돌아볼 수 있습니다.
- 바쁜 현대인들도 간편하게 자기 성찰을 할 수 있으며, 이는 개인의 성장과 행복 추구에 기여할 수 있습니다.

---

## 생성형 AI 활용

![image](https://github.com/user-attachments/assets/5b9adc52-16f9-4d52-af53-c02785b1ad64)


본 프로젝트에서는 다음과 같은 두 가지 주요 AI를 활용했습니다:

1. **대화 상대방의 인격을 생성하는 AI**
   - GPT-4 turbo 기반 인격체 생성 AI를 사용하여 가상의 NPC가 자연스럽게 반응하도록 구현했습니다.
   - 생성된 인격체는 TTS 서비스를 통해 자연스러운 음성 출력이 가능하며, 실존 인물과의 대화처럼 실감나는 경험을 제공합니다.

2. **다이어리 자동 생성 AI**
   - 사용자와 NPC 간의 대화를 수집하고 요약하여 다이어리 형식으로 자동 생성하는 AI를 구현했습니다.
   - 이 AI는 사용자 맞춤형 다이어리를 제공하여 메타버스 공간과 현실의 연결성을 높입니다.

---

## BM 및 수익화 방안

1. **팝업 광고**
   - 창작 관련 제품 및 글쓰기 공모전, 동아리, 출판부 홍보 등을 위한 광고 배치

2. **작성자 후원 기능**
   - 감상자가 마음에 드는 작품에 대해 후원할 수 있는 기능 제공

3. **오프라인 상품**
   - 사용자 작성 글 중 원하는 글을 책으로 출판하여 판매하는 기능 제공

---


## 참고 문헌

- [GPT-4 turbo](https://openai.com/)  
- [Maxon Cinema 4D](https://www.maxon.net/)  
- [Unity Documentation](https://unity.com/)
