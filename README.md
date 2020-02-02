Aquarium
=============
> 온라인 TPS 물총 게임입니다.

📝 Introduction
------------
Aquarium은 TPS 물총 게임으로, 팀 데스매치 게임입니다!  
일반 FPS와 달리 체력이 습도량이기에 체력은 0부터 시작합니다.  
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

  - 팀  
    +  :heart: Team Ruby (루비 팀)  
      루비라는 보석은 사랑이라는 뜻을 가지고있습니다.  
      그래서 고유 팀 특성으로, 체력이 120이 주어집니다! ( 사파이어 체력 100 )  
      
    +  :blue_heart: Team Sapphire (사파이어 팀)  
      사파이어라는 보석은 행운이라는 뜻을 가지고있습니다.   
      그래서 고유 팀 특성으로, 20%의 확률로 크기가 크고 데미지도 큰 물방울이 발사됩니다!  
        
  - 날씨
    + 날씨의 영향으로 플레이어의 습도량에 변화가 옵니다!  
    
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
    + 플레이어의 습도량이 높을수록 플레이어의 옷은 하늘색으로 물들게 됩니다!  
      상대방의 습도량을 알기 위해서는 플레이어의 옷 색상을 유심히 보면 됩니다.   
      
      ![Color](https://user-images.githubusercontent.com/44610250/73602956-bcfd2c00-45bf-11ea-819d-8ebd5490268a.gif)    
      

