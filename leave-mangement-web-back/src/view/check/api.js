import {server} from '@/tools/servers'

/**
 * 申请
 */
export class CheckApi {
  //获取待审核请假列表
  static getCheckingList(data={}){
      return server.connection('POST','api/Approval/GetCheckingList',data)
  }
  static GetApplyJobList(data={}){
      return server.connection('POST','api/Approval/GetApplyJobList',data)
  }
  //提交审核
  static checkApplication(data={}){
      return server.connection('PUT','api/Approval/CheckApplication',data)
  }
  //查看申请
  static getApplication(id){
    return server.connection('POST','api/Approval/GetApplicationById?id='+id)
  }
  //提交上一级
  static pushCheck(data={}){
      return server.connection('POST','api/Approval/PushCheck',data)
  }
}