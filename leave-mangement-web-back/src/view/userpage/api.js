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
}