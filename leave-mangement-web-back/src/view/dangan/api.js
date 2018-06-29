import {server} from '@/tools/servers'

/**
 * 登录
 */
export class FileApi {
  // 获取公司信息
  static getCompany () {
    return server.connection('GET', 'api/File/GetCompanyInfo')
  }
  //获取员工列表
  static getWorkers(data={}){
    return server.connection('POST','api/File/GetWorkList',data)
  }
  //获取到部门列表
  static getDeparmentList(data={}){
    return server.connection('POST','api/File/GetDeparmentList',data)
  }
  //员工列表，获取部门选择器数据
  static getDeparments(){
    return server.connection('GET','api/File/GetDeparments')
  }
  //添加部门时，获取经理的下拉列表
  static getWorkerList(){
    return server.connection('GET','api/File/GetWorkerList')
  }
  //单个添加部门
  static addSingleDep(data={}){
    return server.connection('POST','api/File/AddSingleDpearment',data)
  }
  //编辑部门资料
  static editDeparment(data={}){
    return server.connection('PUT','api/File/EditDeparment',data)
  }
  //删除部门
  static deleteDeparment(id){
    return server.connection('DELETE',`api/File/DeleteDeparment?id=`+id)
  }
  //获取员工状态列表
  static getStateList(){
    return server.connection('GET','api/File/GetStateList')
  }
  //删除员工状态
  static deleteState(id){
    return server.connection('DELETE',`api/File/DeleteStateById?id=`+id)
  }
  //新增员工状态
  static addState(data={}){
    return server.connection('POST','api/File/AddState',data)
  }
  //编辑员工状态
  static editState(data={}){
    return server.connection('PUT','api/File/EditState',data)
  }
  //获取职位
  static getPositionList(){
    return server.connection('GET','api/File/GetPositionList')
  }
  //删除职位
  static deletePosition(id){
    return server.connection('DELETE',`api/File/DeletePositionById?id=`+id)
  }
  //新增职位
  static addPosition(data={}){
    return server.connection('POST','api/File/AddPosition',data)
  }
  //编辑职位
  static editPosition(data={}){
    return server.connection('PUT','api/File/EditPosition',data)
  }
  //获取一个员工详细信息
  static getWorkerMessage(id){
    return server.connection('POST',`api/User/GetWorkerById?userId=`+id)
  }
}