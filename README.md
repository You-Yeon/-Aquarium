Aquarium
=============
> 온라인 TPS 물총 게임입니다.

📝 Introduction
------------
Aquarium은 도시에서 대결하는 TPS 물총 게임으로, 팀 데스매치 게임입니다!  
일반 FPS와 달리 체력이 습도량이기에 체력은 0부터 시작합니다. 또한 제한시간은 10분입니다.  
물에 많이 맞을수록 습도량은 올라가고 최대치에 도달하면 쓰러지기때문에,  
상대방 팀의 물을 최대한 피하고 상대방을 물총으로 맞춰 쓰러트리는 서바이벌 TPS 게임입니다!  

:computer: Developerment skill
------------
- 라이브러리 : ProudNet
- 언어 : C#, C++
- 데이터베이스 : MSSQL
- 프로토콜 : WebSocket
- 게임 엔진 : Unity v2019.1.10f1
- IDE : Visual Studio 2019, Microsoft SQL Management Studio

:gun: Description
-----------

* ### 물총 게임  
  - 캐릭터 선택  
    + Game Start를 누르면 다양한 캐릭터를 선택할 수 있습니다.  
      캐릭터를 선택하면 자동으로 게임 방과 팀이 매칭이 되어집니다.      
      ( 팀은 Sapphire, Ruby 두개의 팀으로 나뉩니다. )
        
      ![Character](https://user-images.githubusercontent.com/44610250/73602381-a30b1b80-45b6-11ea-9fb6-64f13fe5f3d8.gif)  
    
  - 게임 시작  
    + 4명의 플레이어가 입장하면 3초의 카운트가 시작되고 자동으로 게임이 시작됩니다.  
      만약 카운트가 끝나기전에 플레이어가 나가면 카운트가 종료되고 다시 플레이어를 기다립니다.  
        
      ![GameStart](https://user-images.githubusercontent.com/44610250/73602506-748e4000-45b8-11ea-87b9-95a7ed0ca0c7.gif)   
    
  - 컨트롤  
    + WASD 로 플레이어의 이동이 가능하고 마우스 클릭으로 총알을 발사합니다.  
      R키로는 장전을 하고, ESC키로 마우스가 밖으로 나오게 할 수 있습니다.  
    
      ![Control](https://user-images.githubusercontent.com/44610250/73603499-ef5e5780-45c6-11ea-9047-86447fa6cae4.gif)  
    
  - 게임 맵  
    + 게임 맵은 도시입니다.  
      분홍색 영역에서만 플레이어는 돌아다닐 수 있습니다.  
      빨간색과 파란색은 각 팀의 리스폰 지역입니다. 노란색은 아이템의 리스폰 지역입니다.  
      
      ![설명지도](https://user-images.githubusercontent.com/44610250/73603301-fa63b880-45c3-11ea-99e3-2559ab01cea1.png)  

  - 팀  
    +  :heart: Team Ruby (루비 팀)  
      루비라는 보석은 사랑이라는 뜻을 가지고있습니다.  
      그래서 고유 팀 특성으로, 체력이 120이 주어집니다! ( 사파이어 체력 100 )  
      
    +  :blue_heart: Team Sapphire (사파이어 팀)  
      사파이어라는 보석은 행운이라는 뜻을 가지고있습니다.   
      그래서 고유 팀 특성으로, 20%의 확률로 크기가 크고 데미지도 큰 물방울이 발사됩니다!  
        
  - 날씨  
    + 날씨의 영향으로 플레이어의 습도량에 변화가 옵니다!  
      날씨는 게임 시작시 랜덤으로 결정됩니다.  
    
        - 맑은 아침:sunrise_over_mountains:  
          맑은 아침 태양의 영향으로 플레이어의 습도량에 기화현상이 생깁니다! ( 초당 습도량 -1 )  
           
      ![Morning](https://user-images.githubusercontent.com/44610250/73602849-b66db500-45bd-11ea-8266-8838160c5c27.png)    
      
        - 흐린 저녁:city_sunset:  
          흐린 저녁에는 플레이어에게 딱 알맞는 날씨입니다! ( 초당 습도량 변화없음 )  
          
      ![Evening](https://user-images.githubusercontent.com/44610250/73602851-b968a580-45bd-11ea-825c-163598ec2a07.png)   
      
      
        - 습한 새벽:city_sunrise:  
          습한 새벽에는 주변에 생긴 이슬의 영향으로 플레이어의 습도량에 변화가 생깁니다! ( 초당 습도량 +1 ) 
          
      ![Dawn](https://user-images.githubusercontent.com/44610250/73602853-ba99d280-45bd-11ea-995f-a4f03d19a328.png)     
        
  - 색깔  
    + 플레이어의 습도량이 높을수록 플레이어의 색상이 하늘색으로 물들게 됩니다!  
      상대방의 습도량을 알기 위해서는 플레이어의 색상을 유심히 보면 알 수 있습니다.   
      
      ![Color](https://user-images.githubusercontent.com/44610250/73602956-bcfd2c00-45bf-11ea-819d-8ebd5490268a.gif)    
      
  - 채팅  
    + ESC를 누르면 마우스가 생겨 플레이어들과 채팅으로 소통할 수 있습니다!  
      다시 게임으로 돌아가고 싶다면 게임창을 누르면 됩니다.  

      ![Chat](https://user-images.githubusercontent.com/44610250/73603131-c38ca300-45c1-11ea-80ae-8f12a04326de.gif)   
    
  - 아이템  
    + 아이템은 물병입니다! 그래서 플레이어의 물총의 물을 채우게 되죠.  
      아이템은 게임맵에 있던 리스폰 지역중에서 5개가 랜덤으로 생성됩니다!  
      즉, 게임 시작시 10군데 중 5군데가 아이템 리스폰지역으로 결정되고, 그 자리에서만 아이템이 생성됩니다.   
      아이템은 25의 물방울을 채워주고, 10초 후에 다시 생성이 됩니다.  
      
      ![Item](https://user-images.githubusercontent.com/44610250/73603573-c1c5de00-45c7-11ea-8fff-b7d612e5357b.gif)   
       
  - 플레이어 무적  
    + 플레이어는 처음 게임 시작할 때, 죽고나서 다시 태어날 때 5초간 무적상태입니다.  
      무적이 된 플레이어는 노란색을 띄게 됩니다.
    
      ![invincibility](https://user-images.githubusercontent.com/44610250/73603658-1f0e5f00-45c9-11ea-91a3-ea1d538fb5ae.gif)  
  
  - 게임 방 도중 입장 시스템  
    + 게임 도중 플레이어가 나가게 된다면, 그 자리를 채우기위해 다른 플레이어가 입장할 수 있습니다!  
      하지만 도중에 입장하는 것이 싫을 수 있으니 선택권이 주어지고, 5분 이상 시간이 남은 방만 알려줍니다.  
      Yes를 선택하면 게임을 입장하고, No를 선택하면 시작하지 않은 방으로 갑니다.  
      입장하게 되면 해당 방의 플레이어들, 시간, 팀 점수를 얻습니다.  
        
      ![Select2](https://user-images.githubusercontent.com/44610250/73604163-90511080-45cf-11ea-926c-195a2953d2ce.gif)  
      
  - 승패  
    + 타이머가 끝나면 게임이 종료되고 점수를 통해 승패처리를 합니다.  
      만약에 점수가 같으면, 플레이어들의 총 습도량을 계산하여 습도량이 적은 팀이 우승합니다.  
      정말 만약에 습도량 조차도 같아버리면, 무승부 처리를 합니다.  
        
      ![END](https://user-images.githubusercontent.com/44610250/73603974-5979fb00-45cd-11ea-9595-c799935d653d.gif)  

  - 로그인  
    + 로그인 기능이 있습니다.   
      해당 DB에 존재하지 않는 유저, 아이디와 비밀번호 불일치시 다시 로그인 화면으로 돌아갑니다.  
     
      ![Login](https://user-images.githubusercontent.com/44610250/73604220-43216e80-45d0-11ea-9848-87f9c9d8bb80.gif)  
      
    
