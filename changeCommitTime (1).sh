git filter-branch --env-filter \
    'if [ $GIT_COMMIT =  33e0f900255e192eb858c38151fa2bfbeb27913d ]
     then
         export GIT_AUTHOR_DATE="Fri Apr 26 12:45:50 2019 -0000"
         export GIT_COMMITTER_DATE="Fri Apr 26 12:45:50 2019 -0000"
     fi'
