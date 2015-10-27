
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banana.Entity.Db;
using System.Data;
using Dapper;

namespace Banana.Dal.Db
{
    public class UsersDal : DalBase
    {
        #region Auto

        /// <summary>
        /// 通过主键查询
        /// </summary>
        public Users GetByPrimaryKey(Int32 primaryKey)
        {
            string sql = "select * from [Users] with(nolock) where [id] = @id";
            object param = new { id= primaryKey };
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Users>(sql, param);
                return r.FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(Users entity)
        {
            string sql = @"insert into [Users]
                               ([userName], [userPwd], [mobile], [version], [userType], [addTime], [userIP], [remoteHost], [userAgent], [imsi], [imei], [lastLoginTime], [pno], [udid], [headUrl], [nickName], [sex], [birthday], [provinceId], [cityId], [valid], [collegeId], [realName], [bananaCount], [address])
                               values
                               (@userName, @userPwd, @mobile, @version, @userType, @addTime, @userIP, @remoteHost, @userAgent, @imsi, @imei, @lastLoginTime, @pno, @udid, @headUrl, @nickName, @sex, @birthday, @provinceId, @cityId, @valid, @collegeId, @realName, @bananaCount, @address)";
        
            object param = new
            {
                userName = entity.UserName, 
                userPwd = entity.UserPwd, 
                mobile = entity.Mobile, 
                version = entity.Version, 
                userType = entity.UserType, 
                addTime = entity.AddTime, 
                userIP = entity.UserIP, 
                remoteHost = entity.RemoteHost, 
                userAgent = entity.UserAgent, 
                imsi = entity.Imsi, 
                imei = entity.Imei, 
                lastLoginTime = entity.LastLoginTime, 
                pno = entity.Pno, 
                udid = entity.Udid, 
                headUrl = entity.HeadUrl, 
                nickName = entity.NickName, 
                sex = entity.Sex, 
                birthday = entity.Birthday, 
                provinceId = entity.ProvinceId, 
                cityId = entity.CityId, 
                valid = entity.Valid, 
                collegeId = entity.CollegeId, 
                realName = entity.RealName, 
                bananaCount = entity.BananaCount, 
                address = entity.Address
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param);
                return count;
            }
       }
        /// <summary>
        /// 添加一条记录 返回自增ID
        /// </summary>
        public int AddAndReturn(Users entity)
        {
            string sql = @"insert into [Users]
                               ([userName], [userPwd], [mobile], [version], [userType], [addTime], [userIP], [remoteHost], [userAgent], [imsi], [imei], [lastLoginTime], [pno], [udid], [headUrl], [nickName], [sex], [birthday], [provinceId], [cityId], [valid], [collegeId], [realName], [bananaCount], [address])
                               values
                               (@userName, @userPwd, @mobile, @version, @userType, @addTime, @userIP, @remoteHost, @userAgent, @imsi, @imei, @lastLoginTime, @pno, @udid, @headUrl, @nickName, @sex, @birthday, @provinceId, @cityId, @valid, @collegeId, @realName, @bananaCount, @address)";
        
            object param = new
            {
                userName = entity.UserName, 
                userPwd = entity.UserPwd, 
                mobile = entity.Mobile, 
                version = entity.Version, 
                userType = entity.UserType, 
                addTime = entity.AddTime, 
                userIP = entity.UserIP, 
                remoteHost = entity.RemoteHost, 
                userAgent = entity.UserAgent, 
                imsi = entity.Imsi, 
                imei = entity.Imei, 
                lastLoginTime = entity.LastLoginTime, 
                pno = entity.Pno, 
                udid = entity.Udid, 
                headUrl = entity.HeadUrl, 
                nickName = entity.NickName, 
                sex = entity.Sex, 
                birthday = entity.Birthday, 
                provinceId = entity.ProvinceId, 
                cityId = entity.CityId, 
                valid = entity.Valid, 
                collegeId = entity.CollegeId, 
                realName = entity.RealName, 
                bananaCount = entity.BananaCount, 
                address = entity.Address
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                 int row = conn.Execute(sql, entity);
                    if (row > 0)
                        return conn.Query<Users>("SELECT @@IDENTITY AS Id").Single<Users>().Id;
                    else
                        return -1;
            }
       }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        public int Add(Users entity, IDbTransaction tran)
        {
            string sql = @"insert into [Users]
                               ([userName], [userPwd], [mobile], [version], [userType], [addTime], [userIP], [remoteHost], [userAgent], [imsi], [imei], [lastLoginTime], [pno], [udid], [headUrl], [nickName], [sex], [birthday], [provinceId], [cityId], [valid], [collegeId], [realName], [bananaCount], [address])
                               values
                               (@userName, @userPwd, @mobile, @version, @userType, @addTime, @userIP, @remoteHost, @userAgent, @imsi, @imei, @lastLoginTime, @pno, @udid, @headUrl, @nickName, @sex, @birthday, @provinceId, @cityId, @valid, @collegeId, @realName, @bananaCount, @address)";
            
            object param = new
            {
                userName = entity.UserName, 
                userPwd = entity.UserPwd, 
                mobile = entity.Mobile, 
                version = entity.Version, 
                userType = entity.UserType, 
                addTime = entity.AddTime, 
                userIP = entity.UserIP, 
                remoteHost = entity.RemoteHost, 
                userAgent = entity.UserAgent, 
                imsi = entity.Imsi, 
                imei = entity.Imei, 
                lastLoginTime = entity.LastLoginTime, 
                pno = entity.Pno, 
                udid = entity.Udid, 
                headUrl = entity.HeadUrl, 
                nickName = entity.NickName, 
                sex = entity.Sex, 
                birthday = entity.Birthday, 
                provinceId = entity.ProvinceId, 
                cityId = entity.CityId, 
                valid = entity.Valid, 
                collegeId = entity.CollegeId, 
                realName = entity.RealName, 
                bananaCount = entity.BananaCount, 
                address = entity.Address
            };
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
       }
       
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Users> GetAll(string fields, string where, object param, string orderBy)
        {
            string sql = String.Format("select {0} from [Users] with(nolock) ", String.IsNullOrEmpty(fields) ? "*" : fields); 
            if (!String.IsNullOrEmpty(where))
                sql += " where " + where;
                
            if (!String.IsNullOrEmpty(orderBy))
                sql += " order by " + orderBy;

            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.Query<Users>(sql, param);
                return r.ToList();
            }
        }
        
        /// <summary>
        /// 获取所有
        /// </summary>
        public IList<Users> GetAll(string fields, int pageIndex, int pageSize, string where, object param, string orderBy, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(where))
                where = " where " + where;

            sql.AppendFormat("select count(*) from [Users] with(nolock) {0}", where);
            sql.AppendLine();
            
            sql.AppendFormat(@"select *
                                  from (select {0}, row_number() over(order by {1}) as rownum
                                          from [Users] with(nolock)
                                          {2} ) as T
                                 where rownum between {3} and {4}", String.IsNullOrEmpty(fields) ? "*" : fields,
                                 orderBy, where, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            
            using (IDbConnection conn = OpenConnection())
            {
                var r = conn.QueryMultiple(sql.ToString(), param);
                recordCount = r.Read<int>().Single();
                IList<Users> list = r.Read<Users>().ToList();
                return list;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where)
        {              
            string sql = String.Format("update [Users] set {0} where {1}", fields, where);
            
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param);
                return count;
            }
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(string fields, object param, string where, IDbTransaction tran)
        {
            string sql = String.Format("update [Users] set {0} where {1}", fields, where);
            
            int count = tran.Connection.Execute(sql, param, tran);
            return count;
        }
        
		public int Update(Users entity)
		{
			//GetUpdateSql2
			  string sql = @"update [Users] set userName=@userName, userPwd=@userPwd, mobile=@mobile, version=@version, userType=@userType, addTime=@addTime, userIP=@userIP, remoteHost=@remoteHost, userAgent=@userAgent, imsi=@imsi, imei=@imei, lastLoginTime=@lastLoginTime, pno=@pno, udid=@udid, headUrl=@headUrl, nickName=@nickName, sex=@sex, birthday=@birthday, provinceId=@provinceId, cityId=@cityId, valid=@valid, collegeId=@collegeId, realName=@realName, bananaCount=@bananaCount, address=@address  where id=@id ";
			   object param = new
            {
               id = entity.Id, 
               userName = entity.UserName, 
               userPwd = entity.UserPwd, 
               mobile = entity.Mobile, 
               version = entity.Version, 
               userType = entity.UserType, 
               addTime = entity.AddTime, 
               userIP = entity.UserIP, 
               remoteHost = entity.RemoteHost, 
               userAgent = entity.UserAgent, 
               imsi = entity.Imsi, 
               imei = entity.Imei, 
               lastLoginTime = entity.LastLoginTime, 
               pno = entity.Pno, 
               udid = entity.Udid, 
               headUrl = entity.HeadUrl, 
               nickName = entity.NickName, 
               sex = entity.Sex, 
               birthday = entity.Birthday, 
               provinceId = entity.ProvinceId, 
               cityId = entity.CityId, 
               valid = entity.Valid, 
               collegeId = entity.CollegeId, 
               realName = entity.RealName, 
               bananaCount = entity.BananaCount, 
               address = entity.Address
            };
            
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param);
                return count;
            }
		}
		/// <summary>
        /// 删除
        /// </summary>
        public int DeleteList(string where)
        {
            string sql=String.Format("delete from [Users] where {0}",where);
             using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql);
                return count;
            }
           
        }
        
        /// <summary>
        /// 获取 file max
        /// </summary>
        public double GetMaxField(string filed)
        {
            string sql=String.Format("Select CAST(MAX({0}) AS float) from [Users] ",filed);
              using (IDbConnection conn = OpenConnection())
            {
                try
                {
                  return conn.Query<double>(sql).Single();
                }catch{
                  return 0.0d;
                }
            }
        }
        
        
        #endregion
        
        #region Extend
        #endregion
    }
}

