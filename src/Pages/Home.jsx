import React, { useContext } from 'react';
import { context } from '../Context/Store';

function Home() {
  const { posts } = useContext(context);

  return (
    <div>
      <h2>Posts</h2>
      {posts.length === 0 ? (
        <p>No posts found.</p>
      ) : (
        posts.map((post, index) => (
          <div className='d-block justify-content-center pb-5' key={index} style={{ borderBottom:'1px solid lightgrey', padding: '10px', marginBottom: '20px' }}>
            <h4>Posted by: {post.name}</h4>
            <br />
            {post.postPicUrl && (
              <div className='mb-3' style={{ display: 'flex', justifyContent: 'center' }}>
                <img
                  src={post.postPicUrl}
                  alt="Post"
                  style={{ width: '700px', maxHeight: '400px', objectFit: 'cover' }}
                />
              </div>
            )}
            <h3>{post.caption}</h3>
            <p>Tags: {post.tags.join(', ')}</p>
            <small >Created: {new Date(post.createdAt).toLocaleString()}</small>
          </div>

        ))
      )}
    </div>
  );
}

export default Home;
