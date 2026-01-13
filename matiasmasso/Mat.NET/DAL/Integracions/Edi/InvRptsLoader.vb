Namespace Integracions.Edi

    Public Class InvRptLoader
        Shared Function Update(exs As List(Of Exception), oInvRpt As DTO.Integracions.Edi.Invrpt) As Boolean
            Dim retval As Boolean

            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try
                UpdateHeader(oInvRpt, oTrans)
                UpdateItems(oInvRpt, oTrans)
                UpdateExceptions(oInvRpt, oTrans)
                UpdateLog(oInvRpt, oTrans)
                oTrans.Commit()
                retval = True
            Catch ex As Exception
                oTrans.Rollback()
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try

            Return retval
        End Function

        Shared Sub Update(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            UpdateHeader(oInvRpt, oTrans)
            UpdateItems(oInvRpt, oTrans)
            UpdateExceptions(oInvRpt, oTrans)
            UpdateLog(oInvRpt, oTrans)
        End Sub

        Shared Sub UpdateHeader(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM EdiInvrptHeader ")
            sb.AppendLine("WHERE Guid='" & oInvRpt.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Guid") = oInvRpt.Guid
            Else
                oRow = oTb.Rows(0)
            End If

            Dim oInterlocutor = oInvRpt.Interlocutors.FirstOrDefault(Function(x) x.Qualifier = DTO.Integracions.Edi.Invrpt.Interlocutor.Qualifiers.Reporting)
            With oInvRpt
                oRow("NadMs") = .NadMs
                oRow("NadGy") = .NadGy
                oRow("DocNum") = .DocNum
                oRow("Fch") = SQLHelper.NullableFch(.Fch)
                If oInterlocutor Is Nothing OrElse oInterlocutor.Guid Is Nothing Then
                    oRow("Customer") = System.DBNull.Value
                Else
                    oRow("Customer") = oInterlocutor.Guid
                End If
            End With

            oDA.Update(oDs)
        End Sub

        Shared Sub UpdateItems(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            DeleteItems(oInvRpt, oTrans)

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM EdiInvrptItem ")
            sb.AppendLine("WHERE Parent='" & oInvRpt.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each item In oInvRpt.Items
                Dim oRow = oTb.NewRow
                oRow("Parent") = oInvRpt.Guid
                oRow("Lin") = item.Lin
                oRow("Ean") = item.SkuId(DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Ean)
                oRow("RefSupplier") = Left(item.SkuId(DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Supplier), 20)
                oRow("RefCustomer") = Left(item.SkuId(DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Customer), 20)
                oRow("Qty") = item.Qty
                oTb.Rows.Add(oRow)
            Next

            oDA.Update(oDs)
        End Sub

        Shared Sub UpdateExceptions(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM EdiversaExceptions ")
            sb.AppendLine("WHERE Parent='" & oInvRpt.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each ex In oInvRpt.Exceptions
                AddRow(oTb, ex, oInvRpt.Guid)
            Next
            For Each item In oInvRpt.Items
                For Each ex In item.Exceptions
                    AddRow(oTb, ex, item.Guid)
                Next
            Next
            oDA.Update(oDs)
        End Sub

        Shared Sub AddRow(ByRef oTb As DataTable, ex As DTO.Integracions.Edi.Exception, parentGuid As Guid)
            Dim oRow = oTb.NewRow
            oRow("Parent") = parentGuid
            oRow("Cod") = ex.Cod
            oRow("Msg") = ex.Msg
            oRow("Tag") = SQLHelper.NullableString(ex.Tag)
            oRow("TagCod") = SQLHelper.NullableInt(ex.TagCod)
            oTb.Rows.Add(oRow)
        End Sub

        Shared Sub UpdateLog(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("UPDATE Edi ")
            sb.AppendLine("SET Result = 1, ResultGuid = '" & oInvRpt.Guid.ToString() & "' ")
            sb.AppendLine("WHERE Edi.Guid ='" & oInvRpt.File.ToString & "' ")
            Dim SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End Sub

        Shared Function Delete(exs As List(Of Exception), oInvRpt As DTO.Integracions.Edi.Invrpt) As Boolean
            Dim retval As Boolean

            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try
                InvRptLoader.Delete(oInvRpt, oTrans)
                oTrans.Commit()
                retval = True
            Catch ex As Exception
                oTrans.Rollback()
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try

            Return retval

        End Function

        Shared Sub Delete(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            RestoreLog(oInvRpt, oTrans)
            DeleteExceptions(oInvRpt, oTrans)
            DeleteItems(oInvRpt, oTrans)
            DeleteHeader(oInvRpt, oTrans)
        End Sub


        Shared Sub DeleteHeader(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE EdiInvRptHeader ")
            sb.AppendLine("WHERE Guid='" & oInvRpt.Guid.ToString & "' ")
            Dim SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End Sub
        Shared Sub DeleteItems(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE EdiInvRptItem ")
            sb.AppendLine("WHERE Parent='" & oInvRpt.Guid.ToString & "' ")
            Dim SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End Sub
        Shared Sub DeleteExceptions(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE EdiversaExceptions ")
            sb.AppendLine("WHERE Parent='" & oInvRpt.Guid.ToString & "' ")
            Dim SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End Sub

        Shared Sub RestoreLog(oInvRpt As DTO.Integracions.Edi.Invrpt, ByRef oTrans As SqlTransaction)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("UPDATE Edi ")
            sb.AppendLine("SET Result = 0, ResultGuid = NULL ")
            sb.AppendLine("WHERE Edi.ResultGuid ='" & oInvRpt.Guid.ToString & "' ")
            Dim SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End Sub

        Shared Function Raport(customer As Guid, sku As Guid, fch As DateTime) As String
            Dim retval As String = ""
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM EdiInvrptItem ")
            sb.AppendLine("INNER JOIN EdiInvrptheader on EdiInvrptItem.parent = EdiInvrptheader.Guid ")
            sb.AppendLine("INNER JOIN Edi ON EdiInvrptheader.Guid = Edi.ResultGuid ")
            sb.AppendLine("WHERE EdiInvrptItem.Sku='" & sku.ToString() & "' ")
            sb.AppendLine("AND EdiInvrptHeader.Customer='" & customer.ToString() & "' ")
            sb.AppendLine("AND EdiInvrptHeader.Fch='" & fch.ToString("yyyy-MM-dd HH:mm:ss") & "' ")
            Dim SQL = sb.ToString
            Dim oDrd = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim src As String = oDrd("Text")
                Dim oInvRpt = DTO.Integracions.Edi.Invrpt.Factory(src)

                sb = New System.Text.StringBuilder
                sb.AppendLine(String.Format("Missatge Edi {0} rebut el {1:dd/MM/yy HH:mm:ss}", oDrd("Filename"), oDrd("FchCreated")))
                sb.AppendLine(String.Format("Document de client {0} del {1:dd/MM/yy HH:mm:ss}", oInvRpt.DocNum, oInvRpt.Fch))
                sb.AppendLine(String.Format("Linia {0}", oDrd("lin")))

                Dim item = oInvRpt.Items.FirstOrDefault(Function(x) x.Lin = oDrd("Lin"))
                If item Is Nothing Then
                    sb.AppendLine(String.Format("Linia no trobada al fitxer"))
                Else
                    For Each skuId In item.SkuIds
                        sb.AppendLine(String.Format("{0}: {1}", skuId.Qualifier.ToString, skuId.Id))
                    Next
                    sb.AppendLine(String.Format("Stock: {0}", item.Qty))
                End If
                retval = sb.ToString
            Else
                retval = "No s'ha trobat el registre. Pot ser que s'hagi eliminat el fitxer Edi original"
            End If
            oDrd.Close()
            Return retval
        End Function

    End Class
    Public Class InvRptsLoader

        Shared Function Model(oHolding As DTOHolding, oUser As DTOUser, fch As Nullable(Of Date)) As DTO.Models.InvrptModel
            Dim retval As New DTO.Models.InvrptModel
            Dim oProductGuids As New HashSet(Of Guid)
            Dim oLang = oUser.Lang
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EdiInvrptHeader.Guid, EdiInvrptHeader.DocNum, EdiInvrptHeader.Customer AS CustomerGuid, EdiInvrptHeader.Fch ")
            sb.AppendLine(", (CASE WHEN CliClient.Ref IS NULL THEN CliGral.RaoSocial ELSE CliClient.Ref END) AS CustomerNom, VwSkuNom.BrandGuid ")
            sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryCodi, VwSkuNom.SkuGuid, VwSkuNom.BrandNomEsp, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuNomEsp ")
            sb.AppendLine(", EdiInvrptItem.Qty, VwRetail.Retail ")
            sb.AppendLine("FROM   EdiInvrptHeader ")
            sb.AppendLine("		INNER JOIN (SELECT Customer, MAX(Fch) AS LastFch ")
            sb.AppendLine("		FROM    EdiInvrptHeader AS EdiInvrptHeader_1 ")

            If fch IsNot Nothing Then
                sb.AppendLine("WHERE EdiInvrptHeader_1.Fch < '" & String.Format("{0:yyyy-MM-dd 00:00:00.000}", CType(fch, DateTime).AddDays(1)) & "' ")
            End If

            sb.AppendLine("		GROUP BY Customer) AS X ON EdiInvrptHeader.Customer = X.Customer AND EdiInvrptHeader.Fch = X.LastFch ")
            sb.AppendLine("INNER JOIN CliGral ON EdiInvrptHeader.Customer = CliGral.Guid ")
            sb.AppendLine("INNER JOIN CliClient ON EdiInvrptHeader.Customer = CliClient.Guid ")
            sb.AppendLine("INNER JOIN EdiInvrptItem ON EdiInvrptHeader.Guid = EdiInvrptItem.Parent ")
            sb.AppendLine("INNER JOIN VwSkuNom ON EdiInvrptItem.Sku = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwRetail ON EdiInvrptItem.Sku = VwRetail.SkuGuid ")

            If oUser.Rol.id = DTORol.Ids.manufacturer Then
                oLang = DTOLang.ENG
                sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString() & "' ")
            End If

            sb.AppendLine(" WHERE VwSkuNom.Emp = " & oUser.Emp.Id & " ")
            sb.AppendLine(" ORDER BY CustomerNom, VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNom, VwSkuNom.SkuNom DESC")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oProductGuid = SQLHelper.GetGuidFromDataReader(oDrd("SkuGuid"))
                If Not oProductGuids.Contains(oProductGuid) Then
                    Dim brandNom = SQLHelper.GetStringFromDataReader(oDrd("BrandNomEsp"))
                    Dim categoryNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor").Tradueix(oLang)
                    Dim skuNom = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor").Tradueix(oLang)
                    Dim oProduct = ProductLoader.NewProduct(DTOProduct.SourceCods.Sku, CType(oDrd("BrandGuid"), Guid), brandNom, oDrd("CategoryGuid"), categoryNom, oDrd("SkuGuid"), skuNom)
                    retval.Catalog.AddProduct(oProduct)
                    oProductGuids.Add(oProductGuid)
                End If

                Dim oCustomer As DTOGuidNom.Compact = Nothing
                If Not IsDBNull(oDrd("CustomerGuid")) Then
                    oCustomer = DTOGuidNom.Compact.Factory(oDrd("CustomerGuid"), SQLHelper.GetStringFromDataReader(oDrd("CustomerNom")))
                    If Not retval.Customers.Any(Function(x) x.Guid.Equals(oDrd("CustomerGuid"))) Then
                        retval.Customers.Add(oCustomer)
                    End If
                End If

                Dim oItem As New Models.InvrptModel.Item
                With oItem
                    .Qty = oDrd("Qty")
                    .Fch = oDrd("Fch")
                    .Ean = SQLHelper.GetStringFromDataReader(oDrd("Ean"))
                    .Retail = SQLHelper.GetDecimalFromDataReader(oDrd("Retail"))
                    If oCustomer IsNot Nothing Then .CustomerGuid = oCustomer.Guid
                    If oProductGuid <> Nothing Then .ProductGuid = oProductGuid
                End With
                retval.Items.Add(oItem)
            Loop
            oDrd.Close()

            retval.Fchs = Fchs(oHolding)
            Return retval
        End Function

        Shared Function Exceptions() As List(Of DTO.Integracions.Edi.Exception)
            Dim retval As New List(Of DTO.Integracions.Edi.Exception)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EdiversaExceptions.Guid AS ExceptionGuid, X.Guid AS ItemGuid, X.Parent AS DocGuid, EdiInvrptHeader.DocNum, EdiInvrptHeader.Fch ")
            sb.AppendLine(", EdiversaExceptions.Cod AS ExceptionCod, EdiversaExceptions.Msg AS ExceptionMsg ")
            sb.AppendLine(", EdiversaExceptions.Tag AS ExceptionTag, EdiversaExceptions.TagCod AS ExceptionTagCod ")
            sb.AppendLine(", Edi.Guid AS FileGuid ")
            sb.AppendLine("FROM EdiversaExceptions ")
            sb.AppendLine("INNER JOIN ( ")
            sb.AppendLine("     SELECT ediInvrptHeader.Guid, ediInvrptHeader.Guid AS Parent ")
            sb.AppendLine("     FROM EdiInvrptHeader ")
            sb.AppendLine("     UNION SELECT EdiInvrptItem.Guid, EdiInvrptItem.Parent ")
            sb.AppendLine("     FROM EdiInvrptItem ")
            sb.AppendLine("     ) X ON EdiversaExceptions.Parent = X.Guid ")
            sb.AppendLine("INNER JOIN ediInvrptHeader ON X.Parent = ediInvrptHeader.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Edi ON X.Parent = Edi.ResultGuid ")
            sb.AppendLine("ORDER BY EdiInvrptHeader.Fch DESC, EdiInvrptHeader.DocNum DESC ")
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oItem As New DTO.Integracions.Edi.Exception
                With oItem
                    .FileTag = DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008
                    .FileGuid = SQLHelper.GetGuidFromDataReader(oDrd("FileGuid"))
                    .Guid = oDrd("ExceptionGuid")
                    .Msg = SQLHelper.GetStringFromDataReader(oDrd("ExceptionMsg"))
                    .DocGuid = oDrd("DocGuid")
                    .DocNum = oDrd("DocNum")
                    .Fch = oDrd("Fch")
                    .Cod = oDrd("ExceptionCod")
                    .Tag = SQLHelper.GetStringFromDataReader(oDrd("ExceptionTag"))
                    .TagCod = SQLHelper.GetIntegerFromDataReader(oDrd("ExceptionTagCod"))
                End With
                retval.Add(oItem)
            Loop
            oDrd.Close()

            Return retval
        End Function


        Shared Function Fchs(oHolding As DTOHolding) As HashSet(Of Date)
            Dim retval As New HashSet(Of Date)

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CONVERT (date, Fch) AS Fch ")
            sb.AppendLine("FROM EdiInvrptHeader ")
            sb.AppendLine("GROUP BY CONVERT (date, Fch) ")
            sb.AppendLine("ORDER BY CONVERT (date, Fch) DESC ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                retval.Add(oDrd("Fch"))
            Loop
            oDrd.Close()
            Return retval
        End Function

        Shared Function Update(exs As List(Of Exception), oInvRpts As List(Of DTO.Integracions.Edi.Invrpt)) As Boolean
            Dim retval As Boolean

            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try
                For Each oInvRpt In oInvRpts
                    InvRptLoader.Update(oInvRpt, oTrans)
                Next
                oTrans.Commit()
                retval = True
            Catch ex As Exception
                oTrans.Rollback()
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try

            Return retval

        End Function



        Shared Function Delete(exs As List(Of Exception), oInvRpts As List(Of DTO.Integracions.Edi.Invrpt)) As Boolean
            Dim retval As Boolean

            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try
                For Each oInvRpt In oInvRpts
                    InvRptLoader.Delete(oInvRpt, oTrans)
                Next
                oTrans.Commit()
                retval = True
            Catch ex As Exception
                oTrans.Rollback()
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try

            Return retval

        End Function

        Shared Function All() As List(Of DTO.Integracions.Edi.Invrpt)
            Dim retval As New List(Of DTO.Integracions.Edi.Invrpt)

            Dim SQL As String = "SELECT Guid,Text from edi where tag='INVRPT_D_96A_UN_EAN004'  order by fch"
            Dim oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim src As String = SQLHelper.GetStringFromDataReader(oDrd("Text"))
                If src.Contains("NADMS") Then
                    Dim oEdi = DTO.Integracions.Edi.Invrpt.Factory(oDrd("Text"))
                    oEdi.Guid = oDrd("Guid")
                    retval.Add(oEdi)
                End If
            Loop
            oDrd.Close()
            Return retval
        End Function




    End Class

End Namespace