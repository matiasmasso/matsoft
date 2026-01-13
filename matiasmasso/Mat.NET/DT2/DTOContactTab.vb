Public Class DTOContactTab

    Property Contact As DTOContact
    Property Tab As Tabs

    Public Enum Tabs
        GRAL = 0
        CLI = 10
        CREDIT = 20
        PRV = 30
        STAFF = 60
        MGZ = 300
        MAIL = 5
        USR = 900
        REP = 50
        BANC = 90
        VISA = 201
        SPV = 202
        TRP = 18
        ESHOP = 203
    End Enum


    Public Overrides Function ToString() As String
        Dim retval As String = ""
        Select Case _Tab
            Case Tabs.GRAL
                retval = "GENERAL"
            Case Tabs.CLI
                retval = "CLIENT"
            Case Tabs.CREDIT
                retval = "CREDIT"
            Case Tabs.PRV
                retval = "PROVEIDOR"
            Case Tabs.STAFF
                retval = "PERSONAL"
            Case Tabs.MGZ
                retval = "MAGATZEM"
            Case Tabs.MAIL
                retval = "CORRESPONDENCIA"
            Case Tabs.USR
                retval = "USUARI"
            Case Tabs.REP
                retval = "REPRESENTANT"
            Case Tabs.BANC
                retval = "BANC"
            Case Tabs.VISA
                retval = "VISA"
            Case Tabs.TRP
                retval = "TRANSPORTISTA"
            Case Else
                retval = "DESCONEGUT"
        End Select
        Return retval
    End Function
End Class
