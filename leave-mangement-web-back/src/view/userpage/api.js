import {server} from '@/tools/servers'

/**
 * 登录
 */
export class UserPageApi {
  //获取一个员工详细信息
  static getWorkerMessage(id){
    return server.connection('POST',`api/User/GetWorkerById?userId=`+id)
  }
  //修改密码
  static modifyPassword(data={}){
    return server.connection('PUT','api/User/ModifyPassword',data)
  }
  //完善用户信息
  static editUserMessage(data={}){
    return server.connection('PUT','api/User/EditUserMessage',data)
  }

  //上传头像
  static uploadImg(base64={}){
    return server.connection('POST',`api/User/UpdateUserImg`,base64)
  }

  //获取员工的出勤统计
  static getAttence(){
    return server.connection('GET','api/Attendance/AttendanceByWorker')
  }

  static getAttenceInfo(data={}){
    return server.connection('GET','api/Attendance/AttendanceByMonth',data)
  }
}