import {server} from '@/tools/servers'

/**
 * 申请
 */
export class ApprovalApi {
  //获取到部门
  static getDeparment(id){
      return server.connection('POST',`api/File/GetDeparment?id=`+id)
  }
  // 添加申请
  static addApplication (data={}) {
      console.log(data)
    return server.connection('POST', 'api/Approval/AddApplication', data)
  }
  //获取未提交的申请
  static getUnApplicationList(data={}){
      return server.connection('POST','api/Approval/GetUnApplicationList',data)
  }
  //提交申请
  static submitApplication(id){
      return server.connection('POST','api/Approval/SubmitApplication?id='+id)
  }
  //提交编辑申请
  static editApplication(data={}){
    return server.connection('POST','api/Approval/EditApplication',data)
  }
  //删除未提交申请
  static deleteApplication(id){
    return server.connection('DELETE',`api/Approval/DeleteApplicationById?id=`+id)
  }
  //获取已经提交的申请
  static getApplicationList(data={}){
    return server.connection('POST','api/Approval/GetApplicationList',data)
  }
  //查看申请
  static getApplication(id){
      return server.connection('POST','api/Approval/GetApplicationById?id='+id)
  }
  //获取当月请假提交次数
  static getApplicationCount(){
    return server.connection('GET','api/Approval/GetApprovalCount')
  }
}