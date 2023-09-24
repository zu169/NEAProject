Module Module1
    Dim conn As New System.Data.Odbc.OdbcConnection("DRIVER={MySQL ODBC 5.3 ANSI Driver};SERVER=localhost;PORT=3306;DATABASE=csgo game data;USER=root;PASSWORD=root;OPTION=3;")
    Dim MRN As Double = 0
    Dim number As Integer = 0
    Dim gamesstored As Integer = 0
    Dim newaverage(2) As Average
    Dim newGame(64) As GameData
    Dim newProgress(16) As Progress
    Dim newgun As New GunData
    Dim newuser As New user

    Public Class errorhandling
        Inherits Exception
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
    End Class

    Public Class user
        Private username As String
        Private steamID As String
        Private steamkey As String
        Private knownCode As String
        Private password As String

        Public Function getusername() As String
            Return username
        End Function

        Public Sub inputusername(ByVal name As String)
            username = name
        End Sub

        Public Sub inputsteamID(ByVal input As String)
            steamID = input
        End Sub

        Public Sub inputsteamKey(ByRef input As String)
            steamkey = input
        End Sub

        Public Sub inputknowncode(ByRef input As String)
            knownCode = input
        End Sub

        Public Sub checkusername1()
            Dim numbers() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
            Dim firstchar As Char = Mid(username, 1, 1)
            Dim x As Integer = username.Length - 2
            Dim lastpart1 As String = Mid(username, x, 1)
            Dim lastpart2 As String = Mid(username, x + 1, 1)
            Dim result1 As Boolean = False
            Dim result2 As Boolean = False
            Dim choice As Integer
            For i = 0 To numbers.Length - 1
                If lastpart1 = numbers(i) Then
                    result1 = True
                End If
                If lastpart2 = numbers(i) Then
                    result2 = True
                End If
            Next
            Try
                If username.Length > 10 Then
                    Throw New errorhandling("The username is invalid please try again")
                ElseIf username.Length < 5 Then
                    Throw New errorhandling("The username is invalid please try again")
                ElseIf firstchar <> UCase(firstchar) Then
                    Throw New errorhandling("The username is invalid please try again")
                ElseIf result1 Or result2 = False Then
                    Throw New errorhandling("The username is invalid please try again")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                creatingUser(0)
            End Try
            Try
                If CheckIfRepeated() = True Then
                    Throw New errorhandling("The username you entered already exists.")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                Try
                    Console.WriteLine("Would you like to try another username or would you like to log in? (1,2)")
                    choice = Console.ReadLine
                    If choice = 1 Then
                        creatingUser(0)
                    ElseIf choice = 2 Then
                        loginInUser()
                    Else
                        Throw New errorhandling("Invalid request to subroutine.")
                    End If
                Catch exc As errorhandling
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine(exc.Message)
                    Console.ReadKey()
                    Console.ForegroundColor = ConsoleColor.Gray
                    creatingUser(0)
                End Try
            End Try
        End Sub

        Public Sub checkSteamID()
            'Maybe use the APIs to check if the SteamID exists.
            Try
                Console.WriteLine("Please enter your SteamID again. ")
                If Console.ReadLine = steamID Then
                    Console.WriteLine("Your SteamID has been saved.")
                Else
                    Throw New errorhandling("The SteamIDs you entered do not match. Please try again, IT IS VERY IMPORTANT THEY ARE CORRECT.")
                    Console.WriteLine("Input the correct steamID.")
                    steamID = Console.ReadLine
                    checkSteamID()
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                checkSteamID()
            End Try
        End Sub

        Public Function checkusername2(ByVal time As Integer) As Integer
            Dim numbers() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
            Dim firstchar As Char = Mid(username, 1, 1)
            Dim x As Integer = username.Length - 2
            Dim lastpart1 As String = Mid(username, x, 1)
            Dim lastpart2 As String = Mid(username, x + 1, 1)
            Dim result1 As Boolean = False
            Dim result2 As Boolean = False
            For i = 0 To numbers.Length - 1
                If lastpart1 = numbers(i) Then
                    result1 = True
                End If
                If lastpart2 = numbers(i) Then
                    result2 = True
                End If
            Next
            Try
                If username.Length > 10 Then
                    Throw New errorhandling("The username is invalid please try again")
                ElseIf username.Length < 5 Then
                    Throw New errorhandling("The username is invalid please try again")
                ElseIf firstchar <> UCase(firstchar) Then
                    Throw New errorhandling("The username is invalid please try again")
                ElseIf result1 Or result2 = False Then
                    Throw New errorhandling("The username is invalid please try again")
                ElseIf CheckIfRepeated() = False Then
                    Throw New errorhandling("The username isn't found in the database")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                loginInUser()
            End Try
            Return time + 1
        End Function

        Public Sub SaveUsertoDatabase()
            conn.Close()
            conn.Open()
            Dim sql1 As New Odbc.OdbcCommand("INSERT INTO users VALUES ('" & username & "', '" & steamID & "', '" & steamkey & "', '" & knownCode & "')", conn)
            sql1.ExecuteNonQuery()
            conn.Close()
        End Sub

        Public Sub GetUserInfoFromDatabase(ByVal name As String)
            conn.Close()
            conn.Open()
            Dim x As String = "username"
            Dim rsUser As Odbc.OdbcDataReader
            Dim userData As String = "SELECT " & x & " FROM users WHERE username = '" & name & "'"
            Dim sql1 As New Odbc.OdbcCommand(userData, conn)
            rsUser = sql1.ExecuteReader
            Try
                If rsUser.Read = vbNull Then
                    Throw New errorhandling("The username you entered was not found in the database. Please check if it is correct")
                Else
                    x = "steamid"
                    steamID = rsUser.Read
                    x = "steamkey"
                    steamkey = rsUser.Read
                    x = "knowncode"
                    knownCode = rsUser.Read
                    username = name
                End If
                conn.Close()
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                login()
            End Try
        End Sub

        Public Function CheckIfRepeated() As Boolean
            conn.Close()
            conn.Open()
            Dim repeated As Boolean = False
            Dim rsUser As Odbc.OdbcDataReader
            Dim userData As String = "SELECT username FROM users WHERE username = '" & username & "';"
            Dim sql1 As New Odbc.OdbcCommand(userData, conn)
            rsUser = sql1.ExecuteReader
            If rsUser.Read Then
                repeated = True
            Else
                repeated = False
            End If
            Return repeated
            conn.Close()
        End Function
    End Class

    Public Class GameData
        Private Unique_Game_Number As Double
        Private Game_Date As String
        Private KAD(2) As Integer
        Private Map As String
        Private Most_Used_Weapon As String
        Private Game_Result(1) As Integer

        Public Sub InputUniqueGameNumber(ByVal num As Double)
            Unique_Game_Number = num
        End Sub

        Public Sub InputGameDate(ByVal num As String)
            Game_Date = num
        End Sub

        Public Sub InputKAD(ByVal num As Integer, ByVal index As Integer)
            KAD(index) = num
        End Sub

        Public Sub InputMap(ByVal num As String)
            Map = num
        End Sub

        Public Sub InputMUW(ByVal num As String)
            Most_Used_Weapon = num
        End Sub

        Public Sub InputResult(ByVal num As Integer, ByVal index As Integer)
            'Value 0 is the User's amount of won games and Value 1 is the Opponents amount of won games
            Game_Result(index) = num
        End Sub

        Public Function GetUniqueGameNumber() As Integer
            Return Unique_Game_Number
        End Function

        Public Function GetGameDate() As String
            Return Game_Date
        End Function

        Public Function GetKills() As Integer
            Return KAD(0)
        End Function

        Public Function GetAssists() As Integer
            Return KAD(1)
        End Function

        Public Function GetDeaths() As Integer
            Return KAD(2)
        End Function
        Public Function GetMap() As String
            Return Map
        End Function

        Public Function GetMUW() As String
            Return Most_Used_Weapon
        End Function

        Public Function GetUserWins() As Integer
            Return Game_Result(0)
        End Function

        Public Function GetEnemyWins() As Integer
            Return Game_Result(1)
        End Function
    End Class

    Private Class Progress
        Private OverallGameNumber As Double
        Private MostMUW As String
        Private KADAverage(2) As Integer
        Private WinPercentage As Double
        Private AdviceRecord As Boolean
        Private AdviceList As List(Of Boolean) = New List(Of Boolean)

        Public Sub InputOverallGameNumber(ByVal num As Double)
            OverallGameNumber = num
        End Sub

        Public Sub InputMostMUW(ByVal num As String)
            MostMUW = num
        End Sub

        Public Sub InputKillAverage(ByVal num As Integer)
            KADAverage(0) = num
        End Sub

        Public Sub InputAssistAverage(ByVal num As Integer)
            KADAverage(1) = num
        End Sub

        Public Sub InputDeathAverage(ByVal num As Integer)
            KADAverage(2) = num
        End Sub

        Public Sub InputWinPercentage(ByVal num As Double)
            WinPercentage = num
        End Sub

        Public Sub InputAdviceRecord(ByVal num As Integer)
            If num = 1 Then
                AdviceRecord = True
            ElseIf num = 0 Then
                AdviceRecord = False
            End If
        End Sub

        Public Sub InputOverallAdviceRecord(ByVal num As Boolean)
            AdviceList.Add(num)
        End Sub

        Public Function GetOverallGameNumber() As Double
            Return OverallGameNumber
        End Function

        Public Function GetMostMUW() As String
            Return MostMUW
        End Function

        Public Function GetKillsAverage() As Integer
            Return KADAverage(0)
        End Function

        Public Function GetAssistsAverage() As Integer
            Return KADAverage(1)
        End Function

        Public Function GetDeathsAverage() As Integer
            Return KADAverage(2)
        End Function

        Public Function GetWP() As Double
            Return WinPercentage
        End Function

        Public Function GetAdviceRecord() As Boolean
            Return AdviceRecord
        End Function

        Public Function GetAdviceList() As List(Of Boolean)
            Return AdviceList
        End Function
    End Class

    Private Class GunData
        Private Gun_Entity As String
        Private Magazine_Capacity(1) As Integer
        Private Reload_Time As Double
        Private Damage As Integer
        Private Recoil(1) As Integer
        Private Accurate_Range As Integer
        Private Armor_Penetration As Double
        Private Price As Double
        Private Kill_Award As Integer

        Public Sub InputGunName(ByVal name As String)
            Gun_Entity = name
            InputValues()
        End Sub

        Private Sub InputValues()
            conn.Open()
            Dim x As String
            Dim rsGun As Odbc.OdbcDataReader
            Dim gun As String = "SELECT " & x & " FROM gundata WHERE GunEntity = '" & Gun_Entity & "';"
            Dim sql As New Odbc.OdbcCommand(gun, conn)
            x = "MagazineCapacity1"
            rsGun = sql.ExecuteReader
            Magazine_Capacity(0) = rsGun.Read
            x = "MagazineCapacity2"
            rsGun = sql.ExecuteReader
            Magazine_Capacity(1) = rsGun.Read
            x = "ReloadTime"
            rsGun = sql.ExecuteReader
            Reload_Time = rsGun.Read
            x = "Damage"
            rsGun = sql.ExecuteReader
            Damage = rsGun.Read
            x = "Recoil1"
            rsGun = sql.ExecuteReader
            Recoil(0) = rsGun.Read
            x = "Recoil2"
            rsGun = sql.ExecuteReader
            Recoil(1) = rsGun.Read
            x = "AccurateRange"
            rsGun = sql.ExecuteReader
            Accurate_Range = rsGun.Read
            x = "ArmorPenetration"
            rsGun = sql.ExecuteReader
            Armor_Penetration = rsGun.Read
            x = "Price"
            rsGun = sql.ExecuteReader
            Price = rsGun.Read
            x = "KillAward"
            rsGun = sql.ExecuteReader
            Kill_Award = rsGun.Read
            conn.Close()
        End Sub
        Public Function compareGuns(ByVal num As Integer, ByVal type As Integer) As String
            Dim betterGun(30) As String
            Dim start As Integer = 0
            Dim gunnum As Integer = 0
            Dim index As Integer = 0
            Dim store As Boolean = False
            Dim Gun_Entity2() As String = {“‘weapon_cz75a’”, “‘weapon_deagle’”, “‘weapon_elite’”, “‘‘weapon_fiveseven’”, “‘weapon_glock’”, “‘weapon_hkp2000’”, “‘weapon_p250’”, “‘weapon_revolver’”, “‘weapon_tec9’”, “‘weapon_usp_silencer’”, “‘weapon_mag7’”, “‘weapon_nova’”, “‘weapon_sawedoff’”, ”‘weapon_xm1014’”, “‘weapon_m249’”, “‘weapon_negev’”, “‘weapon_mac10’”, “‘weapon_mp5sd’”, “‘weapon_mp7’”, “‘weapon_mp9’”, “‘weapon_p90’”, “‘weapon_bizon’”, “‘weapon_ump45’”, “‘weapon_ak47’”, “‘weapon_aug’”, “‘weapon_famas’”, “‘weapon_galilar’”, “‘weapon_ma41_silencer’”, “‘weapon_m4a1’”, “‘weapon_sg556’”, “‘weapon_awp’”, “‘weapon_g3sg1’”, “‘weapon_scar20’”, “‘weapon_ssg08’”}
            Dim Magazine_Capacity2(1) As Integer
            Dim Reload_Time2 As Double
            Dim Damage2 As Integer
            Dim Recoil2(1) As Integer
            Dim Accurate_Range2 As Integer
            Dim Armor_Penetration2 As Double
            Dim Price2 As Double
            Dim Kill_Award2 As Integer
            Dim x As String
            Dim data As Odbc.OdbcDataReader
            If num = 0 Then
                For i = 0 To 36 - 1
                    Dim sql1 As New Odbc.OdbcCommand("SELECT " & x & " FROM gundata WHERE GunEntity = " & Gun_Entity2(i) & ";", conn)
                    x = "MagazineCapacity1"
                    data = sql1.ExecuteReader
                    Magazine_Capacity2(0) = data.Read
                    x = "MagazineCapacity2"
                    data = sql1.ExecuteReader
                    Magazine_Capacity2(1) = data.Read
                    x = "ReloadTime"
                    data = sql1.ExecuteReader
                    Reload_Time2 = data.Read
                    x = "Damage"
                    data = sql1.ExecuteReader
                    Damage2 = data.Read
                    x = "Recoil1"
                    data = sql1.ExecuteReader
                    Recoil2(0) = data.Read
                    x = "AccurateRange"
                    data = sql1.ExecuteReader
                    Accurate_Range2 = data.Read
                    x = "ArmorPenetration"
                    data = sql1.ExecuteReader
                    Armor_Penetration2 = data.Read
                    x = "Price"
                    data = sql1.ExecuteReader
                    Price2 = data.Read
                    x = "KillAward"
                    data = sql1.ExecuteReader
                    Kill_Award2 = data.Read
                    store = compareGuns(type, 0)
                    If store = True Then
                        betterGun(gunnum) = Gun_Entity2(i)
                        gunnum += 1
                    End If
                    Do Until betterGun(index) = vbNullString
                        InputGunName(betterGun(index))
                        start += 1
                        For z = start To gunnum - 1
                            Dim sql2 As New Odbc.OdbcCommand("SELECT " & x & " FROM gundata WHERE GunEntity = " & betterGun(z) & ";", conn)
                            x = "MagazineCapacity1"
                            data = sql2.ExecuteReader
                            Magazine_Capacity2(0) = data.Read
                            x = "MagazineCapacity2"
                            data = sql2.ExecuteReader
                            Magazine_Capacity2(1) = data.Read
                            x = "ReloadTime"
                            data = sql2.ExecuteReader
                            Reload_Time2 = data.Read
                            x = "Damage"
                            data = sql2.ExecuteReader
                            Damage2 = data.Read
                            x = "Recoil1"
                            data = sql2.ExecuteReader
                            Recoil2(0) = data.Read
                            x = "AccurateRange"
                            data = sql2.ExecuteReader
                            Accurate_Range2 = data.Read
                            x = "ArmorPenetration"
                            data = sql2.ExecuteReader
                            Armor_Penetration2 = data.Read
                            x = "Price"
                            data = sql2.ExecuteReader
                            Price2 = data.Read
                            x = "KillAward"
                            data = sql2.ExecuteReader
                            Kill_Award2 = data.Read
                            store = compareGuns(type, 0)
                            If store = True Then
                                index += 1
                                Exit For
                            End If
                        Next
                    Loop
                    Return Gun_Entity
                Next
            ElseIf num = 1 Then
                Dim recoil1 As Double
                Dim recoilnew As Double
                'Format the recoil
                recoil1 = Recoil(0) / Recoil(1)
                recoilnew = Recoil2(0) / Recoil2(1)
                'Kills
                If Damage < Damage2 Or recoil1 > recoilnew Then
                    Return True
                End If
            ElseIf num = 2 Then
                'Assists
                If Magazine_Capacity(0) < Magazine_Capacity2(0) Or Armor_Penetration < Armor_Penetration2 Then
                    Return True
                End If
            ElseIf num = 3 Then
                'Deaths 
                If Damage < Damage2 Or Accurate_Range < Accurate_Range2 Then
                    Return True
                End If
            End If
        End Function
        Public Function GetGunEntity() As String
            Return Gun_Entity
        End Function

        Public Function GetMagazineCapacity() As String
            Dim str As String = "The gun holds " & Magazine_Capacity(0) & " bullets in its' magazine and " & Magazine_Capacity(1) & " bullets in its' reserves."
            Return str
        End Function

        Public Function GetReloadTime() As Double
            Return Reload_Time
        End Function

        Public Function GetDamage() As Integer
            Return Damage
        End Function

        Public Function GetRecoil() As String
            Dim perc As Integer = (Recoil(0) / Recoil(1)) * 100
            Dim str As String = Recoil(0) & " out of " & Recoil(1) & " bullets are accurate so the gun is " & perc & "% accurate."
            Return str
        End Function

        Public Function GetAccurateRange() As Integer
            Return Accurate_Range
        End Function

        Public Function GetArmorPenetration() As Double
            Return Armor_Penetration
        End Function

        Public Function GetPrice() As Double
            Return Price
        End Function

        Public Function GetKillAward() As Integer
            Return Kill_Award
        End Function
    End Class

    Public Class Average
        Private x As Integer = 0
        Private MostMUWlist(x) As String
        Private AverageKillslist(x) As Integer
        Private AverageAssistlist(x) As Integer
        Private AverageDeathlist(x) As Integer
        Private AverageUserWinslist(x) As Integer
        Private AverageEnemyWinslist(x) As Integer
        Private WPI As Double
        Private KADincrease As Boolean

        Public Function GetKADincrease() As Boolean
            Return KADincrease
        End Function

        Public Function GetWPI() As Double
            Return WPI
        End Function

        Private Sub index()
            conn.Close()
            conn.Open()
            Dim sql1 As New Odbc.OdbcCommand("SELECT COUNT(UniqueGameNumber) FROM gamedata WHERE username = '" & newuser.getusername & "';", conn)
            x = sql1.ExecuteScalar
            conn.Close()
        End Sub

        Public Sub RetrievingData(ByVal num As Integer)
            index()
            conn.Close()
            conn.Open()
            ReDim MostMUWlist(x)
            ReDim AverageKillslist(x)
            ReDim AverageAssistlist(x)
            ReDim AverageDeathlist(x)
            ReDim AverageUserWinslist(x)
            ReDim AverageEnemyWinslist(x)
            Dim MostMUW As String = "SELECT MostUsedWeapon FROM gamedata WHERE username = '" & newuser.getusername & "';"
            Dim AverageKills As String = "SELECT Kills FROM gamedata WHERE username = '" & newuser.getusername & "';"
            Dim AverageAssists As String = "SELECT Assists FROM gamedata WHERE username = '" & newuser.getusername & "';"
            Dim AverageDeaths As String = "SELECT Deaths FROM gamedata WHERE username = '" & newuser.getusername & "';"
            Dim AverageUserWins As String = "SELECT UserWins FROM gamedata WHERE username = '" & newuser.getusername & "';"
            Dim AverageEnemyWins As String = "SELECT EnemyWins FROM gamedata WHERE username = '" & newuser.getusername & "';"
            Dim count As Integer = 0
            Dim rsProgress As Odbc.OdbcDataReader
            Dim sqlMUW As New Odbc.OdbcCommand(MostMUW, conn)
            Dim sqlKills As New Odbc.OdbcCommand(AverageKills, conn)
            Dim sqlAssists As New Odbc.OdbcCommand(AverageAssists, conn)
            Dim sqlDeaths As New Odbc.OdbcCommand(AverageDeaths, conn)
            Dim sqlUserWins As New Odbc.OdbcCommand(AverageUserWins, conn)
            Dim sqlEnemyWins As New Odbc.OdbcCommand(AverageEnemyWins, conn)
            'Putting the data into arrays
            rsProgress = sqlMUW.ExecuteReader
            count = 0
            Do While rsProgress.Read()
                If count < x - 1 Then
                    MostMUWlist(count) = rsProgress.GetString(0)
                    count += 1
                End If
            Loop
            rsProgress.Close()
            rsProgress = sqlKills.ExecuteReader
            count = 0
            Do While rsProgress.Read
                If count < x Then
                    AverageKillslist(count) = rsProgress.GetInt32(0)
                    count += 1
                End If
            Loop
            rsProgress.Close()
            rsProgress = sqlAssists.ExecuteReader
            count = 0
            Do While rsProgress.Read
                If count < x Then
                    AverageAssistlist(count) = rsProgress.GetInt32(0)
                    count += 1
                End If
            Loop
            rsProgress.Close()
            rsProgress = sqlDeaths.ExecuteReader
            count = 0
            Do While rsProgress.Read
                If count < x Then
                    AverageDeathlist(count) = rsProgress.GetInt32(0)
                    count += 1
                End If
            Loop
            rsProgress.Close()
            rsProgress = sqlUserWins.ExecuteReader
            count = 0
            Do While rsProgress.Read
                If count < x Then
                    AverageUserWinslist(count) = rsProgress.GetInt32(0)
                    count += 1
                End If
            Loop
            rsProgress.Close()
            rsProgress = sqlEnemyWins.ExecuteReader
            count = 0
            Do While rsProgress.Read
                If count < x Then
                    AverageEnemyWinslist(count) = rsProgress.GetInt32(0)
                    count += 1
                End If
            Loop
            rsProgress.Close()
            conn.Close()
            WorkingOutStrAverages(num)
            WorkingOutIntAverages(num, 1)
            WorkingOutIntAverages(num, 2)
            WorkingOutIntAverages(num, 3)
            WorkingOutIntAverages(num, 4)
            SavingDataToDatabase(num)
        End Sub

        Private Sub WorkingOutStrAverages(ByVal num As Integer)
            Dim stringcounts As New Dictionary(Of String, Integer)
            For Each i As String In MostMUWlist
                If i <> vbNullString Then
                    If stringcounts.ContainsKey(i) Then
                        stringcounts(i) += 1
                    Else
                        stringcounts.Add(i, 1)
                    End If
                End If
            Next i
            Dim sortedcount As New Dictionary(Of String, Integer)
            Do Until stringcounts.Count = 0
                Dim maxnumsofar As New KeyValuePair(Of String, Integer)("", 0)
                For Each stringcount As KeyValuePair(Of String, Integer) In stringcounts
                    If stringcount.Value > maxnumsofar.Value Then
                        maxnumsofar = stringcount
                    End If
                Next
                sortedcount.Add(maxnumsofar.Key, maxnumsofar.Value)
                stringcounts.Remove(maxnumsofar.Key)
            Loop
            newProgress(num).InputMostMUW(sortedcount.First.Key)
        End Sub

        Private Sub WorkingOutIntAverages(ByVal num As Integer, ByVal z As Integer)
            Dim len As Integer
            Dim x As Integer = 0
            If z = 1 Then
                len = AverageKillslist.Length
                For i = 0 To len - 1
                    x += AverageKillslist(i)
                Next
                newProgress(num).InputKillAverage(x / len)
            ElseIf z = 2 Then
                len = AverageAssistlist.Length
                For i = 0 To len - 1
                    x += AverageAssistlist(i)
                Next
                newProgress(num).InputAssistAverage(x / len)
            ElseIf z = 3 Then
                len = AverageDeathlist.Length
                For i = 0 To len - 1
                    x += AverageKillslist(i)
                Next
                newProgress(num).InputDeathAverage(x / len)
            ElseIf z = 4 Then
                len = AverageUserWinslist.Length
                Dim arr(len) As Integer
                For i = 0 To len - 1
                    If AverageUserWinslist(i) > AverageEnemyWinslist(i) Then
                        arr(i) = 1
                    ElseIf AverageEnemyWinslist(i) > AverageUserWinslist(i) Then
                        arr(i) = 0
                    ElseIf AverageUserWinslist(i) = AverageEnemyWinslist(i) Then
                        arr(i) = 2
                    End If
                Next
                Dim y As Integer = arr.Length
                Dim n0 As Integer = 0
                Dim n1 As Integer = 0
                Dim n2 As Integer = 0
                For i = 0 To y - 1
                    If arr(i) = 0 Then
                        n0 += 1
                    ElseIf arr(i) = 1 Then
                        n1 += 1
                    ElseIf arr(i) = 2 Then
                        n2 += 1
                    End If
                Next
                Dim lastWP As Double = newProgress(num - 1).GetWP
                Dim currentWP As Double = n1 / (n1 + n0 + n2)
                newProgress(num).InputWinPercentage(currentWP)
                WPI = currentWP - lastWP
            End If
        End Sub

        Private Sub SavingDataToDatabase(ByVal num As Integer)
            index()
            conn.Close()
            conn.Open()
            Try
                Dim num1 As Integer = 0
                Dim Progress As String = "INSERT INTO progress VALUES ('" & x & "','" & newProgress(num).GetMostMUW & "', '" & newProgress(num).GetKillsAverage & "', '" & newProgress(num).GetAssistsAverage & "', '" & newProgress(num).GetDeathsAverage & "', '" & newProgress(num).GetWP & "', '0', '" & newuser.getusername & "');"
                Dim sql1 As New Odbc.OdbcCommand("SELECT MAX(OverallGameNumber) FROM progress WHERE username = '" & newuser.getusername & "';", conn)
                Dim sql2 As New Odbc.OdbcCommand(Progress, conn)
                num1 = sql1.ExecuteScalar
                If x = num1 Then
                    Throw New errorhandling("You have not enterred new data since the last averages were worked so no new averages can't be worked out.")
                Else
                    sql2.ExecuteNonQuery()
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
            End Try
            conn.Close()
        End Sub

        Public Sub DisplayingData(ByVal num As Integer)
            Console.WriteLine()
            Console.WriteLine("Current Averages: ")
            DisplayingAverages(num)
            Console.WriteLine("Win Percentage Change: " & Formatting(5, num))
        End Sub
    End Class

    Sub instructions()
        Console.WriteLine()
        Console.WriteLine("Welcome to the CSGO Data Tracker,")
        Console.WriteLine()
        Console.WriteLine("These are the Instructions, Please read through them carefully and if you have any question don't hesitate to contact me @zzuzanna.03@gmail.com.")
        Console.WriteLine("This program has been designed to collect and save your CSGO game data.")
        Console.WriteLine("You can choose what the program will do with that data after it is collected.")
        Console.WriteLine("You can choose for it to display the data or averages of the data or the prepared advice.")
        Console.WriteLine("If an error is displayed you need to press any key to continue with the code.")
        Console.WriteLine("It is vital that the data you input is in the correct format. The format or the possible inputs will the displayed in brackets alongside the query. ")
        Console.WriteLine()
        Console.WriteLine("After logging in the first thing that you will be allowed to do is input you're game data.")
        Console.WriteLine("Then a menu will be displayed so that you can choose how you want to use the program.")
        Console.WriteLine("After the program has finished executing the option you chose, you will be given the opportunity to choose something else from the menu, input more data or end the program.")
        Console.WriteLine("Thank you for reading through these, I hope you enjoy using my program. ")
        Console.WriteLine()
    End Sub
    Sub gunEntities()
        Dim choice As Char
        Try
            Console.WriteLine("What weapon category would you like displayed? (p-pistols,s-smg,r-rifle,h-heavy)")
            choice = Console.ReadLine
            If choice = "p" Then
                Console.WriteLine("Pistols - ")
                Console.WriteLine()
                Console.WriteLine("Counter Terrorists and Terrorists -")
                Console.WriteLine("P250 - weapon_p250")
                Console.WriteLine("Desert Eagle -  weapon_deagle")
                Console.WriteLine("Dual Berettas - weapon_elite")
                Console.WriteLine("CZ75-Auto - weapon_cz75a")
                Console.WriteLine("R8 Revolver - weapon_revolver")
                Console.WriteLine("Counter Terrorists -")
                Console.WriteLine("USP-S - weapon_usp_silencer")
                Console.WriteLine("P200 - weapon_hkp2000")
                Console.WriteLine("Five-SeveN - weapon_fiveseven")
                Console.WriteLine("Terrorists -")
                Console.WriteLine("Glock-18 - weapon_glock")
                Console.WriteLine("Tec-9 - weapon_tec9")
            ElseIf choice = "s" Then
                Console.WriteLine("SMG - ")
                Console.WriteLine()
                Console.WriteLine("Counter Terrorists and Terrorists -")
                Console.WriteLine("MP7 - weapon_mp7")
                Console.WriteLine("MP5-SD - weapon_mp5sd")
                Console.WriteLine("UMP-45 - weapon_ump45")
                Console.WriteLine("P90 - weapon_p90")
                Console.WriteLine("PP-Bizon - weapon_bizon")
                Console.WriteLine("Counter Terrorists -")
                Console.WriteLine("MP9 - weapon_mp9")
                Console.WriteLine("Terrorists -")
                Console.WriteLine("MAC-10 - weapon_mac10")
            ElseIf choice = "r" Then
                Console.WriteLine("Rifle - ")
                Console.WriteLine()
                Console.WriteLine("Counter Terrorists and Terrorists -")
                Console.WriteLine("SSG 08 - weapon_ssg08")
                Console.WriteLine("AWP - weapon_awp")
                Console.WriteLine("Counter Terrorists -")
                Console.WriteLine("AUG - weapon_aug")
                Console.WriteLine("FAMAS - weapon_famas")
                Console.WriteLine("M4A1-S - weapon_m4a1_silencer")
                Console.WriteLine("M4A1 - weapon_m4a1")
                Console.WriteLine("SCAR-20 - weapon_scar20")
                Console.WriteLine("Terrorists -")
                Console.WriteLine("Galil AR - weapon_galilar")
                Console.WriteLine("AK-47 - weapon_ak47")
                Console.WriteLine("SG 553 - weapon_sg556")
                Console.WriteLine("G3SG1 - weapon_g3sg1")
            ElseIf choice = "h" Then
                Console.WriteLine("Heavy - ")
                Console.WriteLine()
                Console.WriteLine("Counter Terrorists and Terrorists -")
                Console.WriteLine("Nova - weapon_nova")
                Console.WriteLine("XM1014 - weapon_xm1014")
                Console.WriteLine("M249 - weapon_m249")
                Console.WriteLine("Negev - weapon_negev")
                Console.WriteLine("Counter Terrorists -")
                Console.WriteLine("MAG-7 - weapon_mag7")
                Console.WriteLine("Terrorists -")
                Console.WriteLine("Sawed-Off - weapon_sawedoff")
            Else
                Throw New errorhandling("Invalid input.. Try again...")
                gunEntities()
            End If
            Console.ReadKey()
            Dim choice1 As Char
            Console.WriteLine("Would you like to see another category? (y/n)")
            choice1 = Console.ReadLine
            If choice1 = "y" Then
                gunEntities()
            ElseIf choice1 = "n" Then

            Else
                Throw New errorhandling("Invalid input.. Try again...")
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            gunEntities()
        End Try
    End Sub

    Sub graph()
        Dim UGN As Double
        Dim start As Double = 0
        Dim GNend As Double = 0
        'Give the user a choice of the constraint
        Dim choice As Integer = 0
        Try
            Console.WriteLine("Would you like to plot data for (1) all the games or (2) would you like to choose a game number the graph starts from and the game number the graph finishes on. (1,2)")
            choice = Console.ReadLine
            If choice = 1 Then
                UGN = MRN + 1
                GNend = UGN
            ElseIf choice = 2 Then
                Console.WriteLine("What game number would you like the graph to start on? (It can't be less that 0)")
                start = Console.ReadLine
                Console.WriteLine("What game number would you like the graph to end on?")
                UGN = Console.ReadLine
                GNend = UGN - start
                If UGN > MRN + 1 Then
                    Console.WriteLine("The number you entered for the game number the graph should end on is bigger that you overall game number.")
                    Console.WriteLine("So the number will be changed to the maximum possible number.")
                    UGN = MRN + 1
                    GNend = UGN - start
                End If
            Else
                Throw New errorhandling("Invalid input.. Try again...")
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            graph()
        End Try
        'Get the valid data from database
        'Dim the variable which will hold all the information        
        Dim KD(UGN - 1) As Double
        Dim KillArray(GNend - 1) As Integer
        Dim AssistArray(GNend - 1) As Integer
        Dim DeathArray(GNend - 1) As Integer
        Dim KGN(GNend - 1) As Double 'KillGeneralNumber
        Dim AGN(GNend - 1) As Double 'AssistGeneralNumber
        Dim KDRepeats(GNend - 1, GNend - 1) As Double
        Dim count As Double = 0
        Dim getKills As String = "SELECT Kills FROM gamedata WHERE username = '" & newuser.getusername & "' AND UniqueGameNumber = '" & count & "';"
        Dim getAssists As String = "SELECT Assists FROM gamedata WHERE username = '" & newuser.getusername & "' AND UniqueGameNumber = '" & count & "';"
        Dim getDeaths As String = "SELECT Deaths FROM gamedata WHERE username = '" & newuser.getusername & "' AND UniqueGameNumber = '" & count & "';"
        conn.Close()
        conn.Open()
        Dim rsData As Odbc.OdbcDataReader
        Dim SQLkills As New Odbc.OdbcCommand(getKills, conn)
        Dim SQLassists As New Odbc.OdbcCommand(getAssists, conn)
        Dim SQLdeaths As New Odbc.OdbcCommand(getDeaths, conn)
        rsData = SQLkills.ExecuteReader
        Dim v As Double = 0
        count = start
        Do While rsData.Read()
            If count < GNend - 1 Then
                KillArray(count) = rsData.GetInt32(0)
                KGN(v) = count
                v += 1
                count += 1
            End If
        Loop
        rsData = SQLassists.ExecuteReader
        v = 0
        count = start
        Do While rsData.Read()
            If count < GNend - 1 Then
                AssistArray(count) = rsData.GetInt32(0)
                AGN(v) = count
                v += 1
                count += 1
            End If
        Loop
        rsData = SQLdeaths.ExecuteReader
        count = start
        Do While rsData.Read()
            If count < GNend - 1 Then
                DeathArray(count) = rsData.GetInt32(0)
                count += 1
            End If
        Loop
        conn.Close()
        'Work out the KD
        count = start
        For i = 0 To UGN - 1
            Do Until count < GNend - 1
                KD(i) = KillArray(count) / DeathArray(count)
                count += 1
            Loop
        Next
        'Find any repeating KD values
        Dim SelectedKD As Double
        Dim found As Boolean = False
        count = 0
        For i = 0 To KD.Length
            SelectedKD = KD(i)
            count = 0
            For z = i + 1 To KD.Length
                If SelectedKD = KD(z) Then
                    count += 1
                End If
            Next
            For x = 0 To KDRepeats.Length - 1
                If SelectedKD = KDRepeats(x, x) Then
                    found = True
                End If
            Next
            'If found = False Then
            '    KDRepeats(i, i) = SelectedKD,count
            'End If

        Next
    End Sub
    'Sub badgraph()

    '    'X axis will be the Unique Number of the game
    '    'Y axis will the number of kills divided by the number of deaths
    '    'The markings on the graph will be the number of assists
    '    Dim x As String
    '    For v = start To UGN - 1
    '        Dim i As Integer = 0
    '        conn.Open()
    '        x = "Kills"
    '        Dim data As Odbc.OdbcDataReader
    '        Dim getSQL As String = "SELECT " & x & " FROM gamedata WHERE username = '" & newuser.getusername & "' AND UniqueGameNumber = " & v & ";"
    '        Dim sql As New Odbc.OdbcCommand(getSQL, conn)
    '        data = sql.ExecuteReader
    '        KillArray(i) = data.Read
    '        KGN(i) = v
    '        x = "Assists"
    '        data.Close()
    '        data = sql.ExecuteReader
    '        AssistArray(i) = data.Read
    '        AGN(i) = v
    '        x = "Deaths"
    '        data.Close()
    '        data = sql.ExecuteReader
    '        DeathArray(i) = data.Read
    '        conn.Close()
    '        i += 1
    '    Next
    '    For i = 0 To GNend - 1
    '        KD(i) = KillArray(i) / DeathArray(i)
    '    Next
    '    'Sort the arrays
    '    Dim temp As Integer = 0
    '    Dim temp1 As Integer = 0
    '    Dim temp2 As Integer = 0
    '    For times = 0 To GNend - 1
    '        For position = 0 To GNend - 2
    '            If KD(position) < KD(position + 1) Then
    '                temp = KD(position)
    '                temp1 = KGN(position)
    '                temp2 = AssistArray(position)
    '                KillArray(position) = KillArray(position + 1)
    '                KGN(position) = KGN(position + 1)
    '                AssistArray(position) = AssistArray(position)
    '                KillArray(position + 1) = temp
    '                KGN(position + 1) = temp1
    '                AssistArray(position + 1) = temp2
    '            End If
    '        Next
    '        For position = start To UGN - 2
    '            If AGN(position) > AGN(position + 1) Then
    '                temp = AGN(position)
    '                AGN(position) = AGN(position + 1)
    '                AGN(position + 1) = temp
    '            End If
    '        Next
    '    Next
    '    For i = 0 To GNend - 1
    '        For z = i + 1 To GNend - 1
    '            If KD(i) = KD(z) Then
    '                KDRepeats(i, i) = KGN(i)
    '                KDRepeats(i, i + 1) = KGN(z)
    '            End If
    '        Next
    '    Next
    '    For row = 0 To GNend - 1
    '        For col = 0 To GNend - 1

    '        Next
    '    Next
    '    Dim lowestKD As Integer = KD.Min
    '    Dim lowestKDlen As Integer = 0
    '    Dim currentKDlen As Integer = 0
    '    Dim distanceKD As Integer = 0
    '    Dim distance As Integer = 0
    '    Console.WriteLine(lowestKD)
    '    For i = 1 To 7
    '        Do Until Mid(lowestKD, i, 1) = vbNullChar
    '            lowestKDlen += 1
    '        Loop
    '    Next
    '    Console.WriteLine()
    '    Console.WriteLine("KD Graph -")
    '    Console.WriteLine()
    '    Console.WriteLine()
    '    For i = 0 To lowestKDlen + 1
    '        Console.Write(" ")
    '    Next
    '    Console.Write("^")
    '    'Y axis
    '    For i = 0 To GNend - 1
    '        Console.WriteLine(KD(i))
    '        For z = 1 To 6
    '            Do Until Mid(KD(i), i, 1) = vbNullChar
    '                currentKDlen += 1
    '            Loop
    '        Next
    '        Console.Write(KD(i))
    '        For y = 0 To lowestKDlen - currentKDlen + 1
    '            Console.Write(" ")
    '        Next
    '        Console.Write("|")
    '        'repeats
    '        Dim minGN As Double = KGN.Min
    '        If KDRepeats(i, i) <> vbNull Then
    '            For times = 0 To GNend - 1
    '                For position = 0 To GNend - 1
    '                    If KDRepeats(i, position) > KDRepeats(i, position + 1) Then
    '                        temp = KDRepeats(i, position)
    '                        KDRepeats(i, position) = KDRepeats(i, position + 1)
    '                        KDRepeats(i, position + 1) = temp
    '                    End If
    '                Next
    '            Next
    '            For l = 0 To GNend - 1
    '                Do Until KDRepeats(i, l) = vbNull
    '                    distanceKD = KDRepeats(i, l) - minGN
    '                    For u = 0 To distanceKD - 1
    '                        Do Until AGN(u) = temp
    '                            distance += 1
    '                        Loop
    '                    Next
    '                Loop
    '                For q = 0 To (distance * 2) - 1
    '                    Console.Write(" ")
    '                Next
    '                Console.Write(AssistArray(i))
    '                minGN += distance
    '            Next
    '        Else
    '            minGN = KGN.Min
    '            distanceKD = KGN(i) - minGN
    '            For t = 0 To distanceKD - 1
    '                Do Until AGN(t) = KGN(i)
    '                    distance += 1
    '                Loop
    '            Next
    '            For r = 0 To distance - 1
    '                Console.Write(" ")
    '            Next
    '            Console.Write(AssistArray(i))
    '        End If
    '    Next
    '    'Print the X axis
    '    For i = 0 To GNend * 2 - 1
    '        Console.Write("_")
    '    Next
    '    Console.Write(">")
    '    Console.WriteLine()
    '    For i = 0 To GNend - 1
    '        If Mid(AGN(i), 1, 1) <> vbNull Then
    '            Console.Write(Mid(AGN(i), 1, 1))
    '            Console.Write(" ")
    '        End If
    '    Next
    '    Console.WriteLine()
    '    For i = 0 To GNend - 1
    '        If Mid(AGN(i), 2, 1) <> vbNull Then
    '            Console.Write(Mid(AGN(i), 2, 1))
    '            Console.Write(" ")
    '        End If
    '    Next
    '    Console.WriteLine()
    '    For i = 0 To GNend - 1
    '        If Mid(AGN(i), 3, 1) <> vbNull Then
    '            Console.Write(Mid(AGN(i), 3, 1))
    '            Console.Write(" ")
    '        End If
    '    Next
    '    Console.WriteLine()
    '    For i = 0 To GNend - 1
    '        If Mid(AGN(i), 4, 1) <> vbNull Then
    '            Console.Write(Mid(AGN(i), 4, 1))
    '            Console.Write(" ")
    '        End If
    '    Next
    '    Console.WriteLine()
    '    For i = 0 To GNend - 1
    '        If Mid(AGN(i), 5, 1) <> vbNull Then
    '            Console.Write(Mid(AGN(i), 5, 1))
    '            Console.Write(" ")
    '        End If
    '    Next
    '    Console.WriteLine()
    'End Sub

    Function Formatting(ByVal type As Integer, ByVal num As Integer)
        Try
            If type = 1 Then
                'Formatting KAD
                Dim array1(2) As Integer
                array1(0) = newGame(num).GetKills
                array1(1) = newGame(num).GetAssists
                array1(2) = newGame(num).GetDeaths
                Return array1
            ElseIf type = 2 Then
                'Formatting Result
                Dim array2(1) As Integer
                array2(0) = newGame(num).GetUserWins
                array2(1) = newGame(num).GetEnemyWins
                Return array2
            ElseIf type = 3 Then
                'Formatting average KAD
                Dim array3(2) As Integer
                array3(0) = newProgress(num).GetKillsAverage
                array3(1) = newProgress(num).GetAssistsAverage
                array3(2) = newProgress(num).GetDeathsAverage
                Return array3
            ElseIf type = 4 Then
                'Formatting Win Percentage Average
                Dim perc As String
                perc = (newProgress(num).GetWP * 100 & "%")
                Return perc
            ElseIf type = 5 Then
                'Formatting WPI
                Dim perc As String
                perc = (newaverage(0).GetWPI * 100 & "%")
                Return perc
            Else
                Throw New errorhandling("Invalid request to subroutine.")
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
        End Try
    End Function

    Sub generatingAdvice(ByVal num As Integer)
        'gun info    
        Dim newweapon As String
        Dim reloadTime As Double
        Dim damage As Integer
        Dim ap As Double
        Dim ar As Integer
        Dim price As Integer
        Dim ka As Integer
        'displaying variables
        Dim MUW As String = ""
        Dim assists1 As Integer = 0
        Dim deaths As Integer = 0
        Dim UGN As Double = 0
        Dim KD As Integer = newProgress(num).GetKillsAverage / newProgress(num).GetDeathsAverage
        Dim Assists As Integer = newProgress(num).GetAssistsAverage
        Dim Weapon As String = newProgress(num).GetMostMUW
        Dim WPI As Double = newaverage(0).GetWPI
        'This is where I take information from the gundata data about the Weapon stored and depending on the KD give recommended weapon and tactics.
        If KD < 0.5 Then

        ElseIf KD < 1 Then

        ElseIf KD < 0.25 Then

        ElseIf KD > 5 Then
            conn.Close()
            conn.Open()
            Dim data As Odbc.OdbcDataReader
            Dim sql1 As New Odbc.OdbcCommand("SELECT MAX(Deaths) FROM gamedata WHERE username = '" & newuser.getusername & "'", conn)
            Dim sql2 As New Odbc.OdbcCommand("SELECT UniqueGameNumber FROM gamedata WHERE username = '" & newuser.getusername & "' AND Deaths = '" & deaths & "'", conn)
            Dim sql3 As New Odbc.OdbcCommand("SELECT MostUsedWeapon FROM gamedata WHERE username = '" & newuser.getusername & "' AND UniqueGameNumber = '" & UGN & "'", conn)
            Dim sql4 As New Odbc.OdbcCommand("SELECT Assists FROM gamedata WHERE username = '" & newuser.getusername & "' AND Deaths = '" & deaths & "'", conn)
            data = sql1.ExecuteReader
            deaths = data.Read
            data = sql2.ExecuteReader
            UGN = data.Read
            Console.WriteLine("The most deaths you had were " & deaths & ".")
            data = sql3.ExecuteReader
            MUW = data.Read
            conn.Close()
            Console.WriteLine("The weapon you used during the game where you got " & deaths & " deaths was " & MUW & ".")
            newgun.InputGunName(MUW)
            Console.WriteLine(newgun.GetMagazineCapacity)
            reloadTime = newgun.GetReloadTime
            damage = newgun.GetDamage
            Console.WriteLine(newgun.GetRecoil)
            ap = newgun.GetArmorPenetration
            ar = newgun.GetAccurateRange
            price = newgun.GetPrice
            ka = newgun.GetKillAward
            newweapon = newgun.compareGuns(0, 3)
            Console.WriteLine("I recommend that you practice using the gun in custom maps or switching to this weapon " & newweapon & "")
            newgun.InputGunName(newweapon)
            Console.WriteLine(newgun.GetMagazineCapacity)
            Console.WriteLine(newgun.GetRecoil)
            If reloadTime > newgun.GetReloadTime Then
                Console.WriteLine("The reload time for the " & MUW & " is " & reloadTime & " which  is more than the reload time for the " & newweapon & ".")
            ElseIf reloadTime < newgun.GetReloadTime Then
                Console.WriteLine("The reload time for the " & newweapon & " is " & reloadTime & " which  is more than the reload time for the " & MUW & ".")
            ElseIf reloadTime = newgun.GetReloadTime Then
                Console.WriteLine("The reload time, " & reloadTime & "is the same for both guns.")
            End If
            If damage > newgun.GetDamage Then
                Console.WriteLine("The gun " & MUW & " does more damage (" & damage & ") than the recommend gun " & newweapon & " (" & newgun.GetDamage & ").")
            ElseIf damage < newgun.GetDamage Then

            End If
        End If
        If WPI < 0.5 Then

        ElseIf WPI < 0.25 Then

        End If
    End Sub
    Sub DisplayingProgressInformation(ByVal num As Integer)
        'KAD
        Console.WriteLine()
        Console.WriteLine("Here is the graph of your KAD")
        Console.WriteLine("The values along the Y axis are the Kill/Death ratio and the values along the X axis are the unique game number. The number displayed forming the graph are the assists corresponding to the KD and Game number. ")
        'graph()
        Console.WriteLine("Your average KAD is ", Formatting(3, num))
        'Most MUW
        Console.WriteLine("Your Most Used Weapon is " & newProgress(num).GetMostMUW)
        'Win Percentage Average
        Console.WriteLine("Your Win Percentage increase is " & Formatting(4, num))
        generatingAdvice(num)
    End Sub
    Sub DisplayingAverages(ByVal num As Integer)
        Console.WriteLine("Overall Game Number: " & newProgress(num).GetOverallGameNumber)
        Console.WriteLine("Most MUW: " & newProgress(num).GetMostMUW)
        Console.WriteLine("Average KAD: ")
        Dim arr(2) As Integer
        arr = Formatting(3, num)
        Console.Write(arr(0))
        Console.Write("/")
        Console.Write(arr(1))
        Console.Write("/")
        Console.Write(arr(2))
        Console.WriteLine()
        Console.WriteLine("Win Percentage Average: " & Formatting(4, num))
    End Sub
    Sub DisplayingGameData(ByVal num As Integer)
        Console.WriteLine()
        Console.WriteLine("Game Number - " & newGame(num).GetUniqueGameNumber)
        Console.WriteLine("Game Date - " & newGame(num).GetGameDate)
        Console.WriteLine("Game KAD - ")
        Dim arr(2) As Integer
        arr = Formatting(1, num)
        For z = 0 To 2
            Console.Write(arr(z))
            Console.Write("/")
        Next
        Console.WriteLine()
        Console.WriteLine("Game Map - " & newGame(num).GetMap)
        Console.WriteLine("Game Most Used Weapons - " & newGame(num).GetMUW)
        Console.WriteLine("Game Result - ")
        Dim arr1(1) As Integer
        arr1 = Formatting(2, num)
        Console.Write(arr1(0))
        Console.Write("/")
        Console.Write(arr1(1))
        Console.WriteLine()
    End Sub
    Sub displayingdatafromclass(ByVal type As Integer, ByVal requesttype As String, ByVal len As Integer)
        Console.Clear()
        If type = 1 Then
            For i = 0 To number - 1
                Console.WriteLine("Most Recent Game Data - ")
                Console.WriteLine("Game Number - " & newGame(i).GetUniqueGameNumber)
                Console.WriteLine("Game Date - " & newGame(i).GetGameDate)
                Console.WriteLine("Game KAD - ")
                Dim arr(2) As Integer
                arr = Formatting(1, i)
                For z = 0 To 2
                    Console.Write(arr(z))
                    Console.Write("/")
                Next
                Console.WriteLine()
                Console.WriteLine("Game Map - " & newGame(i).GetMap)
                Console.WriteLine("Game Most Used Weapons - " & newGame(i).GetMUW)
                Console.WriteLine("Game Result - ")
                Dim arr1(1) As Integer
                arr1 = Formatting(2, i)
                Console.Write(arr1(0))
                Console.Write("/")
                Console.Write(arr1(1))
            Next
            Console.ReadKey()
        ElseIf type = 2 Then
            'Progress
            Console.WriteLine("Most Recent Averages ")
            'Displaying Most Recent Averages
            DisplayingAverages(0)
            'Working out Current Averages
            newaverage(0).RetrievingData(1)
            'Displaying Current Averages
            newaverage(0).DisplayingData(1)
            DisplayingProgressInformation(0)
            Console.ReadKey()
        ElseIf type = 3 Then
            'Display Game Date Where...
            Dim number1 As Integer = gamesstored
            If Mid(requesttype, 1, 5) = "gDate" Then
                Console.WriteLine("Game Data where Date " & Mid(requesttype, 8, 8) & " :  ")
                For i = number To number1 - 1
                    DisplayingGameData(i)
                Next
            ElseIf Mid(requesttype, 1, 14) = "MostUsedWeapon" Then
                Console.WriteLine("Game Data where Most Used Weapon " & Mid(requesttype, 17, 16) & " :  ")
                For i = number To number1 - 1
                    DisplayingGameData(i)
                Next
            ElseIf Mid(requesttype, 1, 3) = "Map" Then
                Console.WriteLine("Game Data where Most Used Weapon " & Mid(requesttype, 5, 12) & " :  ")
                For i = number To number1 - 1
                    DisplayingGameData(i)
                Next
            End If
            Console.ReadKey()
        End If
        continueiteration()
    End Sub

    Sub choices(ByVal num As Integer, ByRef bool As Boolean)
        Dim choice As Char = "n"
        Dim answer As Char = Console.ReadLine
        If num = 1 Then
            Console.WriteLine("Do you know the correct entity name for the weapon you want to choose? (y/n)")
            choice = Console.ReadLine
            Try
                If choice = "y" Then

                ElseIf choice = "n" Then
                    gunEntities()
                Else
                    Throw New errorhandling("Invalid input.. Try again...")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                choices(1, False)
            End Try
        ElseIf num = 2 Then
            Try
                If answer = "y" Then
                    bool = True
                ElseIf answer = "n" Then
                    bool = False
                Else
                    Throw New errorhandling("Invalid input.. Try again...")
                    choices(2, bool)
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                choices(2, bool)
            End Try
        ElseIf num = 3 Then

        End If
    End Sub

    Sub checkdata(ByVal type As Integer, ByVal strdata As String, ByVal num As Integer)
        Dim value As Integer
        Try
            If type = 1 Then
                'date
                value = Mid(strdata, 1, 4)
                If value < 2012 Or value > 2021 Then
                    Throw New errorhandling("The year you entered was out of bounds")
                ElseIf Mid(strdata, 5, 1) <> "-" Then
                    Throw New errorhandling("Make sure you signify the space between the year, month and day with a '-'")
                End If
                value = Mid(strdata, 6, 2)
                If value < 1 Or value > 12 Then
                    Throw New errorhandling("The month you entered was invalid. Remember there are only 12 months and if it is single digit put a 0 infront.")
                ElseIf Mid(strdata, 8, 1) <> "-" Then
                    Throw New errorhandling("Make sure you signify the space between the year, month and day with a '-'")
                    value = Mid(strdata, 9, 2)
                ElseIf value < 1 Or value > 31 Then
                    Throw New errorhandling("The day you entered is out of bounds. Remember there is a maximum of 31 days in a month.")
                End If
            ElseIf type = 2 Then
                If strdata = "Inferno" Then
                    Console.WriteLine("Press any key to continue.")
                ElseIf strdata = "Cache" Then
                    Console.WriteLine("Press any key to continue.")
                ElseIf strdata = "Mirage" Then
                    Console.WriteLine("Press any key to continue.")
                ElseIf strdata = "Overpass" Then
                    Console.WriteLine("Press any key to continue.")
                ElseIf strdata = "Train" Then
                    Console.WriteLine("Press any key to continue.")
                ElseIf strdata = "Nuke" Then
                    Console.WriteLine("Press any key to continue.")
                ElseIf strdata = "Vertigo" Then
                    Console.WriteLine("Press any key to continue.")
                Else
                    Throw New errorhandling("The map name you entered is incorrect.")
                End If
            ElseIf type = 3 Then
                conn.Close()
                conn.Open()
                Dim str As String
                Dim sql1 As New Odbc.OdbcCommand("SELECT GunEntity FROM gundata WHERE GunEntity = '" & strdata & "'", conn)
                str = sql1.ExecuteScalar
                conn.Close()
                If str = vbNullString Then
                    Throw New errorhandling("The gun entity you entered was invalid please try again.")
                End If
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            generatingAPI(num)
        End Try
    End Sub

    Sub generatingAPI(ByVal opt As Integer)
        Dim Locate As Boolean = False
        Dim UGN As Double = 0
        Dim Result As List(Of Integer) = New List(Of Integer)
        Dim gamedate As String
        Dim map As String
        Dim weapon As String
        If opt = 1 Then
            UGN = MRN
        ElseIf opt = 2 Then
            conn.Close()
            conn.Open()
            'Access the database to get the correct Unique Game Number
            Dim sql1 As New Odbc.OdbcCommand("SELECT COUNT(UniqueGameNumber) FROM gamedata WHERE username = '" & newuser.getusername & "'", conn)
            MRN = sql1.ExecuteScalar()
            UGN = MRN
            conn.Close()
        End If
        'Code which uses the correct api
        'Puts the data into the correct Class
        newGame(number).InputUniqueGameNumber(UGN)
        Console.WriteLine("Input the Game Date. (year-month-day)")
        gamedate = Console.ReadLine
        checkdata(1, gamedate, opt)
        newGame(number).InputGameDate(gamedate)
        Console.WriteLine("Input the number of kills")
        newGame(number).InputKAD(Console.ReadLine, 0)
        Console.WriteLine("Input the number of assists")
        newGame(number).InputKAD(Console.ReadLine, 1)
        Console.WriteLine("Input the number of deaths")
        newGame(number).InputKAD(Console.ReadLine, 2)
        Console.WriteLine("Input the name of the Map")
        map = Console.ReadLine
        checkdata(2, map, opt)
        newGame(number).InputMap(map)
        choices(1, False)
        Console.WriteLine("Input your Most Used Weapon")
        weapon = Console.ReadLine
        checkdata(3, weapon, opt)
        newGame(number).InputMUW(weapon)
        Console.WriteLine("Input the number of your won rounds")
        newGame(number).InputResult(Console.ReadLine, 0)
        Console.WriteLine("Input the number of your lost rounds")
        newGame(number).InputResult(Console.ReadLine, 1)
        number += 1
        inputtingDataToDatabase(1)
        Console.ReadKey()
        Console.WriteLine("Is there another game which needs storing? (y/n)")
        'The Recursive Part of the Subroutine
        choices(2, Locate)
        If Locate = True Then
            gamesstored += 1
            generatingAPI(1)
        ElseIf Locate = False Then
            Menu()
        End If
    End Sub

    Sub inputtingDataToDatabase(ByVal num As Integer)
        conn.Close()
        conn.Open()
        Try
            If num = 1 Then
                'Most Recent Game Data
                For i = 0 To number - 1
                    Dim GameData As String = "INSERT INTO gamedata VALUES ('" & newGame(i).GetUniqueGameNumber & "', '" & newGame(i).GetGameDate & "', '" & newGame(i).GetKills & "', '" & newGame(i).GetAssists & "', '" & newGame(i).GetDeaths & "', '" & newGame(i).GetMap & "', '" & newGame(i).GetMUW & "', '" & newGame(i).GetUserWins & "', '" & newGame(i).GetEnemyWins & "', '" & newuser.getusername & "');"
                    Dim sql1 As New Odbc.OdbcCommand(GameData, conn)
                    sql1.ExecuteNonQuery()
                Next
            ElseIf num = 2 Then
                'User's Overall Progress 
                For i = 0 To number - 1
                    Dim Progress As String = "INSERT INTO progress VALUES ('" & newProgress(i).GetMostMUW & "', '" & newProgress(i).GetKillsAverage & "', '" & newProgress(i).GetAssistsAverage & "', '" & newProgress(i).GetDeathsAverage & "', '" & newProgress(i).GetWP & "', '" & newProgress(i).GetAdviceRecord & "', '" & newuser.getusername & "');"
                    Dim sql2 As New Odbc.OdbcCommand(Progress, conn)
                    sql2.ExecuteNonQuery()
                Next
            ElseIf num = 3 Then
                'Advice Array Data
                For i = 0 To number - 1
                    Dim Advice As String = "INSERT INTO progress VALUES ('" & newProgress(i).GetAdviceRecord & "') WHERE username = " & newuser.getusername & " AND OverallGameNumber = " & MRN & ";"
                    Dim sql3 As New Odbc.OdbcCommand(Advice, conn)
                    sql3.ExecuteNonQuery()
                Next
            Else
                Throw New errorhandling("Invalid request to subroutine.")
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            Menu()
        End Try
        conn.Close()
    End Sub

    Sub retrievingDataFromDatabase(ByVal num As Integer)
        number = gamesstored
        conn.Close()
        conn.Open()
        Dim type As String
        Try
            If num = 1 Then
                'Most Recent Game Data
                retrievingSQL1()
            ElseIf num = 2 Then
                'User's Overall Progress
                retrievingSQL2()
            ElseIf num = 3 Then
                'Date Game Data
                Console.WriteLine("Which date do you want the game data to be based on?")
                type = Console.ReadLine
                retrievingSQL3("gDate = '" & type & "'")
            ElseIf num = 4 Then
                'MUW Game Data
                Dim choice As Char = "n"
                choices(1, False)
                Console.WriteLine("Which weapon do you want the game data to be based on? ")
                type = Console.ReadLine
                retrievingSQL3("MostUsedWeapon = '" & type & "'")
            ElseIf num = 5 Then
                'Map Game Data
                Console.WriteLine("Which map do you want the game data to be based on? (Inferno, Cache, Mirage, Overpass, Train, Nuke, Vertigo )")
                type = Console.ReadLine
                retrievingSQL3("Map = '" & type & "'")
            Else
                Throw New errorhandling("Invalid request to subroutine")
            End If
            conn.Close()
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            Menu()
        End Try
    End Sub
    Function executeSQL(ByVal x As String, ByVal y As Double) As String
        Dim gamedata As String = "SELECT " & x & " FROM gamedata WHERE username = '" & newuser.getusername & "' AND UniqueGameNumber = " & y - 1
        Dim sql2 As New Odbc.OdbcCommand(gamedata, conn)
        Dim rsGame As String
        rsGame = sql2.ExecuteScalar
        Return rsGame
    End Function

    Sub retrievingSQL1()
        conn.Close()
        conn.Open()
        Dim y As Double = 0
        Dim x As String = "gDate"
        Dim sql1 As New Odbc.OdbcCommand("SELECT COUNT(UniqueGameNumber) FROM gamedata WHERE username = '" & newuser.getusername & "'", conn)
        Try
            y = sql1.ExecuteScalar
            If y = vbNull Then
                Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
            Else
                newGame(0).InputUniqueGameNumber(y - 1)
                newGame(0).InputGameDate(executeSQL(x, y))
                x = "Kills"
                newGame(0).InputKAD(executeSQL(x, y), 0)
                x = "Assists"
                newGame(0).InputKAD(executeSQL(x, y), 1)
                x = "Deaths"
                newGame(0).InputKAD(executeSQL(x, y), 2)
                x = "Map"
                newGame(0).InputMap(executeSQL(x, y))
                x = "MostUsedWeapon"
                newGame(0).InputMUW(executeSQL(x, y))
                x = "UserWins"
                newGame(0).InputResult(executeSQL(x, y), 0)
                x = "EnemyWins"
                newGame(0).InputResult(executeSQL(x, y), 1)
                conn.Close()
                gamesstored += 1
                number = gamesstored
                displayingdatafromclass(1, "", 0)
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            generatingAPI(1)
        End Try
    End Sub
    Function executeSQL1(ByVal x As String) As String
        Dim progress As String = ("SELECT " & x & " FROM progress WHERE username = '" & newuser.getusername & "';")
        Dim sql2 As New Odbc.OdbcCommand(progress, conn)
        Dim rsProgress As String
        rsProgress = sql2.ExecuteScalar
        Return rsProgress
    End Function
    Sub retrievingSQL2()
        conn.Close()
        conn.Open()
        Dim y As Double = 0
        Dim x As String = ""
        Dim sql1 As New Odbc.OdbcCommand("SELECT OverallGameNumber FROM progress WHERE username = '" & newuser.getusername & "';", conn)
        Try
            y = sql1.ExecuteScalar
            If y = vbNull Then
                Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
            Else
                newProgress(0).InputOverallGameNumber(y)
                x = "MostMUW"
                newProgress(0).InputMostMUW(executeSQL1(x))
                x = "KillAverage"
                newProgress(0).InputKillAverage(executeSQL1(x))
                x = "AssistAverage"
                newProgress(0).InputAssistAverage(executeSQL1(x))
                x = "DeathAverage"
                newProgress(0).InputDeathAverage(executeSQL1(x))
                x = "WinPercentageIncrease"
                newProgress(0).InputWinPercentage(executeSQL1(x))
                conn.Close()
                displayingdatafromclass(2, "", 0)
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            generatingAPI(1)
        End Try
    End Sub

    Sub retrievingSQL3(ByVal requestType As String)
        conn.Close()
        conn.Open()
        Dim count As Integer
        Dim len As Integer = 0
        Dim sql1 As New Odbc.OdbcCommand("SELECT COUNT(UniqueGameNumber) FROM gamedata WHERE username = '" & newuser.getusername & "';", conn)
        len = sql1.ExecuteScalar
        conn.Close()
        Dim UGNlist(len) As Double
        Dim datelist(len) As String
        Dim killlist(len) As Integer
        Dim assistlist(len) As Integer
        Dim deathlist(len) As Integer
        Dim maplist(len) As String
        Dim MUWlist(len) As String
        Dim userWinslist(len) As Integer
        Dim enemywinslist(len) As Integer
        Dim rsGame As Odbc.OdbcDataReader
        Dim gamedata As String = " "
        conn.Open()
        Try
            gamedata = ("SELECT UniqueGameNumber FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql31 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql31.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    UGNlist(count) = rsGame.GetDouble(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT gDate FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql32 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql32.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    datelist(count) = rsGame.GetString(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT Kills FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql33 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql33.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    killlist(count) = rsGame.GetInt32(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT Assists FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql34 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql34.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    assistlist(count) = rsGame.GetInt32(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT Deaths FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql35 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql35.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    deathlist(count) = rsGame.GetInt32(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT Map FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql36 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql36.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    maplist(count) = rsGame.GetString(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT MostUsedWeapon FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql37 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql37.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    MUWlist(count) = rsGame.GetString(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT UserWins FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql38 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql38.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    userWinslist(count) = rsGame.GetInt32(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            gamedata = ("SELECT EnemyWins FROM gamedata WHERE " & requestType & " AND username = '" & newuser.getusername & "';")
            Dim sql39 As New Odbc.OdbcCommand(gamedata, conn)
            rsGame = sql39.ExecuteReader
            count = 0
            Do While rsGame.Read()
                If count < len - 1 Then
                    enemywinslist(count) = rsGame.GetInt32(0)
                    count += 1
                Else
                    Throw New errorhandling("There are no values that can be used in the database so this procedure cannot executed.")
                End If
            Loop
            rsGame.Close()
            conn.Close()
            gamesstored = 0
            For i = 0 To count
                newGame(number + i).InputUniqueGameNumber(UGNlist(i))
                newGame(number + i).InputGameDate(datelist(i))
                newGame(number + i).InputKAD(killlist(i), 0)
                newGame(number + i).InputKAD(assistlist(i), 1)
                newGame(number + i).InputKAD(deathlist(i), 2)
                newGame(number + i).InputMap(maplist(i))
                newGame(number + i).InputMUW(MUWlist(i))
                newGame(number + i).InputResult(userWinslist(i), 0)
                newGame(number + i).InputResult(enemywinslist(i), 1)
                gamesstored += 1
            Next
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            generatingAPI(1)
        End Try
        displayingdatafromclass(3, requestType, len)
    End Sub
    Sub continueiteration()
        Dim choice As Char = "n"
        Console.WriteLine()
        Console.WriteLine("Would you like to input more data? (y/n)")
        choice = Console.ReadLine
        Try
            If choice = "y" Then
                generatingAPI(2)
            ElseIf choice = "n" Then
                Console.WriteLine("Would you like to go back to the main menu? If you say no this will end the program. (y/n)")
                choice = Console.ReadLine
                If choice = "y" Then
                    Menu()
                ElseIf choice = "n" Then
                    End
                Else
                    Throw New errorhandling("Invalid input... Try again")
                End If
            Else
                Throw New errorhandling("Invalid input... Try again")
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            continueiteration()
        End Try
    End Sub
    Sub MenuChoice1(ByVal num As Integer)
        Console.Clear()
        Dim choice As Char = "n"
        Console.WriteLine("Do you have any new data you would like to input? (y/n)")
        choice = Console.ReadLine
        If choice = "y" Then
            generatingAPI(num)
        ElseIf choice = "n" Then
            Menu()
        End If
    End Sub
    Sub Menu()
        Dim choice As Integer = 0
        Console.Clear()
        Console.WriteLine("Please choose one of the option below -")
        Console.WriteLine()
        Console.WriteLine("1. Display the Most Recent Game Data")
        Console.WriteLine()
        Console.WriteLine("2. Display the User's Overall Progress")
        Console.WriteLine()
        Console.WriteLine("3. Display the prepared advice to the user")
        Console.WriteLine()
        Console.WriteLine("4. Display the game data based on...")
        choice = Console.ReadLine
        Try
            If choice = "1" Then
                retrievingDataFromDatabase(1)
            ElseIf choice = "2" Then
                retrievingDataFromDatabase(2)
            ElseIf choice = "3" Then
                retrievingDataFromDatabase(2)
                'Data will be passed through various algorithms
            ElseIf choice = "4" Then
                Console.WriteLine("1. Display game data based on specific date.")
                Console.WriteLine()
                Console.WriteLine("2. Display game data based on Most Used Weapon.")
                Console.WriteLine()
                Console.WriteLine("3. Display game data based on specific map.")
                choice = Console.ReadLine
                If choice = "1" Then
                    retrievingDataFromDatabase(3)
                ElseIf choice = "2" Then
                    retrievingDataFromDatabase(4)
                ElseIf choice = "3" Then
                    retrievingDataFromDatabase(5)
                Else
                    Throw New errorhandling("Invalid input.. Try again...")
                End If
            Else
                Throw New errorhandling("Invalid input.. Try again...")
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            Menu()
        End Try
    End Sub

    Sub gettingKnownCode(ByVal option1 As Char)
        If option1 = "y" Then
            Console.WriteLine("Copy this link into your browser -")
            Console.WriteLine("https://help.steampowered.com/en/wizard/HelpWithGameIssue/?appid=730&issueid=128")
            Console.WriteLine("Login into your steam account")
            Console.WriteLine("Copy the code which is shown below 'Your most recently completed match token:'")
            gettingKnownCode("n")
        ElseIf option1 = "n" Then
            Console.WriteLine("Please input your correct known code.")
            newuser.inputknowncode(Console.ReadLine)
            Console.WriteLine("You have successfully created your account please remember your username.")
            newuser.SaveUsertoDatabase()
        End If
        MenuChoice1(1)
    End Sub
    Sub creatingUser(ByVal num As Integer)
        Dim ans As Char
        If num = 0 Then
            Console.WriteLine("Creating your account - ")
            Console.WriteLine("The username must have a capital letter at the front and two numbers at the end.")
            Console.WriteLine("It can consist of a max of 10 characters and a min of 5.")
            Console.WriteLine("Please input a username which you will remember.")
            newuser.inputusername(Console.ReadLine)
            newuser.checkusername1()
            Console.WriteLine("Please input your correct steamID.")
            newuser.inputsteamID(Console.ReadLine)
            newuser.checkSteamID()
            Console.WriteLine("Would you like to know how to get your steam authentication code? (y/n)")
            ans = Console.ReadLine
            Try
                If ans = "y" Then
                    Console.WriteLine("Copy this link into your browser -")
                    Console.WriteLine("https://help.steampowered.com/en/wizard/HelpWithGameIssue/?appid=730&issueid=128")
                    Console.WriteLine("Login into your steam account")
                    Console.WriteLine("Then you must copy the authentication code provided exactly")
                ElseIf ans = "n" Then
                Else
                    Throw New errorhandling("Invalid input.. Try again...")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                creatingUser(1)
            End Try
            Console.WriteLine("Please enter your authentication code.")
            newuser.inputsteamKey(Console.ReadLine)
            Console.WriteLine("Do you know how to find your csgo known code? (y/n)")
            ans = Console.ReadLine
            Try
                If ans = "y" Then
                    gettingKnownCode("n")
                ElseIf ans = "n" Then
                    gettingKnownCode("y")
                Else
                    Throw New errorhandling("Invalid input.. Try again...")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                creatingUser(2)
            End Try
        ElseIf num = 1 Then
            Console.WriteLine("Would you like to know how to get your steam authentication code? (y/n)")
            ans = Console.ReadLine
            Try
                If ans = "y" Then
                    Console.WriteLine("Copy this link into your browser -")
                    Console.WriteLine("https://help.steampowered.com/en/wizard/HelpWithGameIssue/?appid=730&issueid=128")
                    Console.WriteLine("Login into your steam account")
                    Console.WriteLine("Then you must copy the authentication code provided exactly")
                    Console.WriteLine("Please enter your authentication code.")
                    newuser.inputsteamKey(Console.ReadLine)
                ElseIf ans = "n" Then
                    Console.WriteLine("Please enter your authentication code.")
                    newuser.inputsteamKey(Console.ReadLine)
                Else
                    Throw New errorhandling("Invalid input.. Try again...")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                creatingUser(1)
            End Try
        ElseIf num = 2 Then
            Console.WriteLine("Do you know how to find your csgo known code? (y/n)")
            ans = Console.ReadLine
            Try
                If ans = "y" Then
                    gettingKnownCode("n")
                ElseIf ans = "n" Then
                    gettingKnownCode("y")
                Else
                    Throw New errorhandling("Invalid input.. Try again...")
                End If
            Catch ex As errorhandling
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(ex.Message)
                Console.ReadKey()
                Console.ForegroundColor = ConsoleColor.Gray
                creatingUser(2)
            End Try
        End If
    End Sub

    Sub loginInUser()
        Dim time As Integer = 1
        Dim name As String
        Console.WriteLine("Please enter your username.")
        name = Console.ReadLine
        newuser.inputusername(name)
        newuser.checkusername2(time)
        newuser.GetUserInfoFromDatabase(name)
        Console.WriteLine("You are now successfully logged in.")
        MenuChoice1(2)
    End Sub

    Sub login()
        Dim ans As Char
        Try
            Console.WriteLine("Is it your first time using the program? (y/n)")
            ans = Console.ReadLine
            If ans = "y" Then
                instructions()
                creatingUser(0)
            ElseIf ans = "n" Then
                loginInUser()
            Else
                Throw New errorhandling("Invalid input.. Try again...")
            End If
        Catch ex As errorhandling
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.Gray
            login()
        End Try
    End Sub
    Sub Main()
        newaverage(0) = New Average
        For i = 0 To newGame.Length - 1
            newGame(i) = New GameData
        Next
        For i = 0 To newProgress.Length - 1
            newProgress(i) = New Progress
        Next
        login()
        Menu()
        Console.ReadKey()
    End Sub
End Module
