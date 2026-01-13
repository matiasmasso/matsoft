Public Class CobramentLoader

    Shared Function Update(oCca As DTOCca, oPnds As List(Of DTOPnd), oImpagats As List(Of DTOImpagat), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            CcaLoader.Update(oCca, oTrans)

            For Each oPnd As DTOPnd In oPnds
                oPnd.Status = DTOPnd.StatusCod.saldat
                PndLoader.SetStatus(oPnd, oTrans)
            Next

            For Each oImpagat As DTOImpagat In oImpagats
                Dim oACompte As DTOAmt = oImpagat.PagatACompte.Clone
                oACompte.Add(oImpagat.Gastos)
                oACompte.Add(oImpagat.Nominal)
                oImpagat.PagatACompte = oACompte.Clone
                oImpagat.Status = DTOImpagat.StatusCodes.Saldat
                ImpagatLoader.Update(oImpagat, oTrans)
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



End Class
